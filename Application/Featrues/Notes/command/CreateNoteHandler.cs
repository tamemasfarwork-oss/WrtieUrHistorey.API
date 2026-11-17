using Application.IRepo;
using Domin.entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Application.Featrues.Notes.command
{
    public sealed class CreateMarginNoteHandler : IRequestHandler<CreateMarginNoteCommand, Guid>
    {
        // الامتداد → نوع الملف (منحدده نحن، ما منثق باللي جاي من الكلاينت)
        private static readonly Dictionary<string, string> AllowedTypes =
            new(StringComparer.OrdinalIgnoreCase)
            {
                [".jpg"] = "image/jpeg",
                [".jpeg"] = "image/jpeg",
                [".png"] = "image/png",
                [".webp"] = "image/webp"
            };

        private const long MaxImageSize = 5 * 1024 * 1024; // 5MB
        private const int MaxImagesCount = 10;

        private readonly IAppDbContext _db;
        private readonly IFileStorage _storage;

        public CreateMarginNoteHandler(IAppDbContext db, IFileStorage storage)
        {
            _db = db;
            _storage = storage;
        }

        public async Task<Guid> Handle(CreateMarginNoteCommand cmd, CancellationToken ct)
        {
            // 1) Validation
            if (string.IsNullOrWhiteSpace(cmd.Text) && cmd.Images.Count == 0)
                throw new ValidationException("Note must have text or at least one image.");

            if (cmd.Images.Count > MaxImagesCount)
                throw new ValidationException($"Max {MaxImagesCount} images per note.");

            foreach (var img in cmd.Images)
            {
                var ext = Path.GetExtension(img.FileName);
                if (!AllowedTypes.ContainsKey(ext))
                    throw new ValidationException($"File type '{ext}' is not allowed.");
                if (img.Length is 0 or > MaxImageSize)
                    throw new ValidationException($"Image '{img.FileName}' size is invalid.");
            }

            var chapterExists = await _db.Chapters.AnyAsync(c => c.Id == cmd.ChapterId, ct);
            if (!chapterExists)
                throw new KeyNotFoundException($"Chapter {cmd.ChapterId} not found.");

            // 2) Build entity
            var now = DateTime.UtcNow;

            var note = new MarginNote
            {
                Id = Guid.NewGuid(),
                ChapterId = cmd.ChapterId,
                Text = string.IsNullOrWhiteSpace(cmd.Text) ? null : cmd.Text.Trim(),
                CreatedAt = now
            };

            // 3) Save files, then DB — rollback files if DB fails
            var savedPaths = new List<string>();
            try
            {
                for (int i = 0; i < cmd.Images.Count; i++)
                {
                    var img = cmd.Images[i];
                    var ext = Path.GetExtension(img.FileName).ToLowerInvariant();

                    var relativePath = await _storage.SaveAsync(
                        img.Content, ext, $"notes/{note.Id}", ct);

                    savedPaths.Add(relativePath);

                    note.Images.Add(new NoteImage
                    {
                        Id = Guid.NewGuid(),
                        MarginNoteId = note.Id,
                        StoragePath = relativePath,
                        ContentType = AllowedTypes[ext],
                        SizeInBytes = img.Length,
                        Order = i,
                        UploadedAt = now
                    });
                }

                await _db.MarginNotes.AddAsync(note, ct);
                await _db.SaveChangesAsync(ct);
            }
            catch
            {
                foreach (var p in savedPaths)
                    await _storage.DeleteAsync(p, CancellationToken.None);
                throw;
            }

            return note.Id;
        }
    }
}

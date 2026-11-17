using Application.IRepo;

namespace Infrastructure
{
    public sealed class LocalFileStorage : IFileStorage
    {
        private readonly string _rootPath;

        public LocalFileStorage(string rootPath)
        {
            _rootPath = Path.GetFullPath(rootPath);
        }

        public async Task<string> SaveAsync(
            Stream content,
            string extension,
            string folder,
            CancellationToken ct)
        {
            var relativePath = Path.Combine(folder, $"{Guid.NewGuid():N}{extension}");
            var fullPath = GetFullPath(relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

            try
            {
                await using var destination = new FileStream(
                    fullPath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                await content.CopyToAsync(destination, ct);
            }
            catch
            {
                if (File.Exists(fullPath))
                    File.Delete(fullPath);
                throw;
            }

            return relativePath;
        }

        public Task DeleteAsync(string relativePath, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var fullPath = GetFullPath(relativePath);
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            return Task.CompletedTask;
        }

        private string GetFullPath(string relativePath)
        {
            var fullPath = Path.GetFullPath(Path.Combine(_rootPath, relativePath));
            var rootWithSeparator = _rootPath.EndsWith(Path.DirectorySeparatorChar)
                ? _rootPath
                : _rootPath + Path.DirectorySeparatorChar;

            if (!fullPath.StartsWith(rootWithSeparator, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("The file path must be inside the storage root.", nameof(relativePath));

            return fullPath;
        }
    }
}

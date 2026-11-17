using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Featrues.Notes.command
{
    public sealed record NoteImageInput(
    Stream Content,
    string FileName,
    string ContentType,
    long Length);

    public sealed record CreateMarginNoteCommand(
    Guid ChapterId,
    string? Text,
    IReadOnlyList<NoteImageInput> Images
    ) : IRequest<Guid>;
}

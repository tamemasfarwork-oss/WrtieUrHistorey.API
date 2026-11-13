using Domin.entities.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Featrues.book.query
{
    public record BookList(Guid Guid, string Title, string? Introduction,
        int PlannedChaptersCount, BookStatus Status, string? FinalReflection,
        int? FinalSatisfaction, DateTime CreatedAt, DateTime? CompletedAt);
}

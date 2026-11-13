using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Featrues.book.command
{
    public record CreateBookCommand(
     Guid UserId,
     string Title,
     string? Introduction,
     int PlannedChaptersCount,
     List<string> Goals
 ) : IRequest<Guid>;
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Featrues.Chapter
{
   public record CreateChapterCommand(
      Guid BookId,
      string Title,
      string? VisibleText,
      string? SealedText,
      
      List<string> Goals
 ) : IRequest<Guid>;
}

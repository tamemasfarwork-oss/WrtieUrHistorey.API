using Application.Featrues.book.command;
using Application.IRepo;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Featrues.Chapter
{
    internal class CreateChapterHandler : IRequestHandler<CreateChapterCommand, Guid>
    {

        readonly private IAppDbContext _context;
       public CreateChapterHandler(IAppDbContext context)
        {
            _context = context;
        }
        public  async Task<Guid> Handle(CreateChapterCommand request, CancellationToken cancellationToken)
        {
            var lastChapterNumber = await _context.Chapters
                .Where(s => s.BookId == request.BookId)
                .MaxAsync(n => (int?)n.Number) ?? 0;

            var chapter = new Domin.entities.Chapter
            {

                Id = Guid.NewGuid(),
                BookId = request.BookId,
                Title = request.Title,
                VisibleText = request.VisibleText ,
                SealedText = request.SealedText ,
                Number = lastChapterNumber + 1,


                Goals = request.Goals.Select((goalText, index) => new Domin.entities.ChapterGoal
                {
                    Id = Guid.NewGuid(),
                    Text = goalText,
                    Order = index + 1 // ترتيب الهدف بالعرض
                }).ToList()
            };

           await  _context.Chapters.AddAsync(chapter);
            return chapter.Id;
        }
    }
}

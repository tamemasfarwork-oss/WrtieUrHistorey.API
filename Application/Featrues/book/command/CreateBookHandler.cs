using Application.IRepo;
using Domin.entities;
using Domin.entities.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

// unit of work 
//  in ca  we leak  between 
namespace Application.Featrues.book.command
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Guid>
    {

        readonly private IAppDbContext _context;
        
        public CreateBookHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateBookCommand command, CancellationToken ct)
        {
            var book = new Book
            {
                UserId = command.UserId,
                Title = command.Title.Trim(),
                Introduction = command.Introduction?.Trim(),
                PlannedChaptersCount = command.PlannedChaptersCount,
                Status = BookStatus.Open,
                CreatedAt = DateTime.UtcNow
            };

            // كل نص بالقائمة بيصير هدف، بترتيب ما كتبه المستخدم
            for (int i = 0; i < command.Goals.Count; i++)
            {
                book.Goals.Add(new BookGoal
                {
                    Text = command.Goals[i].Trim(),
                    Order = i + 1
                });
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync(ct);

            return book.Id;
        }


    }
}

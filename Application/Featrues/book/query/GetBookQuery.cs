using Domin.entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Featrues.book.query
{
    public record GetBookes(Guid guid):IRequest<List<BookList>>;
}

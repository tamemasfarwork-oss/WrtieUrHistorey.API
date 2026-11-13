using Application.Featrues.book.command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CAapi.controller
{
    public class BookController : Controller
    {
        private readonly IMediator _sender; 

        public BookController(IMediator sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand command, CancellationToken ct)
        {
            var id = await _sender.Send(command, ct);
            return Ok(id);
        }
    }
}

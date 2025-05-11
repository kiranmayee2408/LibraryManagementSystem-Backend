using LibraryManagement.Models;
using LibraryManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using LibraryManagement.Hubs;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly IHubContext<BookHub> _hubContext;

        public BooksController(IBookRepository repository, IHubContext<BookHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult GetAll()
        {
            var books = _repository.GetAll();
            return Ok(books);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult GetById(int id)
        {
            var book = _repository.GetById(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.Add(book);
            await _hubContext.Clients.All.SendAsync("BooksUpdated");

            return CreatedAtAction(nameof(GetById), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, Book book)
        {
            if (id != book.BookId)
                return BadRequest("Book ID mismatch.");

            _repository.Update(book);
            await _hubContext.Clients.All.SendAsync("BooksUpdated");

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            _repository.Delete(id);
            await _hubContext.Clients.All.SendAsync("BooksUpdated");

            return NoContent();
        }
    }
}

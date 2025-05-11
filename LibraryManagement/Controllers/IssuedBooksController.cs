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
    public class IssuedBooksController : ControllerBase
    {
        private readonly IIssuedBookRepository _repository;
        private readonly IHubContext<IssuedBookHub> _hubContext;

        public IssuedBooksController(
            IIssuedBookRepository repository,
            IHubContext<IssuedBookHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult GetAll()
        {
            var issuedBooks = _repository.GetAllWithDetails();

            var result = issuedBooks.Select(i => new
            {
                i.IssueId,
                i.BookId,
                i.MemberId,
                i.IssueDate,
                i.DueDate,
                i.ReturnDate,
                BookTitle = i.Book?.Title,
                MemberName = i.Member?.FullName
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult GetById(int id)
        {
            var issued = _repository.GetById(id);
            if (issued == null)
                return NotFound();

            return Ok(new
            {
                issued.IssueId,
                issued.BookId,
                issued.MemberId,
                issued.IssueDate,
                issued.DueDate,
                issued.ReturnDate
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(IssuedBook book)
        {
            if (book.BookId == 0 || book.MemberId == 0)
                return BadRequest("BookId and MemberId are required.");

            _repository.Add(book);
            await _hubContext.Clients.All.SendAsync("IssuedBookUpdated");
            return CreatedAtAction(nameof(GetById), new { id = book.IssueId }, book);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, IssuedBook book)
        {
            if (id != book.IssueId)
                return BadRequest("ID mismatch");

            _repository.Update(book);
            await _hubContext.Clients.All.SendAsync("IssuedBookUpdated");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            _repository.Delete(id);
            await _hubContext.Clients.All.SendAsync("IssuedBookUpdated");
            return NoContent();
        }
    }
}

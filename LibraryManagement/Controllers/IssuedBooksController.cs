using LibraryManagement.Models;
using LibraryManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssuedBooksController : ControllerBase
    {
        private readonly IIssuedBookRepository _repository;

        public IssuedBooksController(IIssuedBookRepository repository)
        {
            _repository = repository;
        }

        // GET: api/IssuedBooks
        [HttpGet]
        public IActionResult GetAll()
        {
            var issuedBooks = _repository.GetAllWithDetails();

            var result = issuedBooks.Select(i => new
            {
                i.IssueId,
                BookTitle = i.Book?.Title,
                MemberName = i.Member?.FullName,
                i.IssueDate,
                i.DueDate,
                i.ReturnDate
            });

            return Ok(result);
        }

        // GET: api/IssuedBooks/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var issued = _repository.GetById(id);
            if (issued == null)
                return NotFound();

            var result = new
            {
                issued.IssueId,
                BookTitle = issued.Book?.Title,
                MemberName = issued.Member?.FullName,
                issued.IssueDate,
                issued.DueDate,
                issued.ReturnDate
            };

            return Ok(result);
        }

        // POST: api/IssuedBooks
        [HttpPost]
        public IActionResult Add(IssuedBook book)
        {
            _repository.Add(book);
            return CreatedAtAction(nameof(GetById), new { id = book.IssueId }, book);
        }

        // PUT: api/IssuedBooks/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, IssuedBook book)
        {
            if (id != book.IssueId)
                return BadRequest();

            _repository.Update(book);
            return NoContent();
        }

        // DELETE: api/IssuedBooks/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return NoContent();
        }
    }
}

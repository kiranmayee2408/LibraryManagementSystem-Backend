using LibraryManagement.Models;
using LibraryManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _repository.GetAll();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = _repository.GetById(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            _repository.Add(book);
            return CreatedAtAction(nameof(GetById), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book book)
        {
            if (id != book.BookId)
                return BadRequest();

            _repository.Update(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return NoContent();
        }
    }
}

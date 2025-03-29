using LibraryManagement.Models;
using LibraryManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var issued = _repository.GetById(id);
            return issued == null ? NotFound() : Ok(issued);
        }

        [HttpPost]
        public IActionResult Add(IssuedBook book)
        {
            _repository.Add(book);
            return CreatedAtAction(nameof(GetById), new { id = book.IssueId }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, IssuedBook book)
        {
            if (id != book.IssueId) return BadRequest();
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

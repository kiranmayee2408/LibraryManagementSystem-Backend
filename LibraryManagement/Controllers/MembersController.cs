using LibraryManagement.Models;
using LibraryManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _repository;

        public MembersController(IMemberRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var member = _repository.GetById(id);
            return member == null ? NotFound() : Ok(member);
        }

        [HttpPost]
        public IActionResult Add(Member member)
        {
            _repository.Add(member);
            return CreatedAtAction(nameof(GetById), new { id = member.MemberId }, member);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Member member)
        {
            if (id != member.MemberId) return BadRequest();
            _repository.Update(member);
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

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
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _repository;
        private readonly IHubContext<MemberHub> _hubContext;

        public MembersController(IMemberRepository repository, IHubContext<MemberHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult GetById(int id)
        {
            var member = _repository.GetById(id);
            return member == null ? NotFound() : Ok(member);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(Member member)
        {
            _repository.Add(member);
            await _hubContext.Clients.All.SendAsync("MembersUpdated");
            return CreatedAtAction(nameof(GetById), new { id = member.MemberId }, member);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, Member member)
        {
            if (id != member.MemberId)
                return BadRequest("Member ID mismatch.");

            _repository.Update(member);
            await _hubContext.Clients.All.SendAsync("MembersUpdated");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            _repository.Delete(id);
            await _hubContext.Clients.All.SendAsync("MembersUpdated");
            return NoContent();
        }
    }
}

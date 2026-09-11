using Microsoft.AspNetCore.Mvc;
using EmployeeAttendancePortalApi.Models;
using EmployeeAttendancePortalApi.Repositories;

namespace EmployeeAttendancePortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeAttendanceController : ControllerBase
    {
        private readonly IEmployeeAttendanceRepository _repo;

        public EmployeeAttendanceController(IEmployeeAttendanceRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeAttendance attendance)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _repo.AddAsync(attendance);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeAttendance attendance)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != attendance.Id && attendance.Id != Guid.Empty)
            {
                return BadRequest("Id in route does not match id in payload.");
            }

            attendance.Id = id;

            var updated = await _repo.UpdateAsync(attendance);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

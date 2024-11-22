using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject5.Application.DTOs;
using MiniProject5.Application.Interfaces.IServices;
using MiniProject5.Application.Services;
using MiniProject5.Persistence.Models;

namespace MiniProject5.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<ActionResult> GetAllDepartments([FromQuery] QueryObjectDepartment query)
        {
            var result = await _departmentService.GetAllDepartmentsAsync(query);
            return Ok(result);
        }

        // GET: api/Department/NoPages
        [HttpGet("NoPages")]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartmentsNoPages()
        {
            var departments = await _departmentService.GetAllDepartmentsNoPagesAsync();
            return Ok(departments);
        }

        // GET: api/Department/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentByIdAsync(id);
                return Ok(department);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // POST: api/Department
        [HttpPost]
        public async Task<ActionResult<Department>> AddDepartment([FromBody] Department department)
        {
            if (department == null)
            {
                return BadRequest(new { message = "Invalid department data." });
            }

            try
            {
                var newDepartment = await _departmentService.AddDepartmentAsync(department);
                return CreatedAtAction(nameof(GetDepartment), new { id = newDepartment.Deptid }, newDepartment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Department/{deptId}
        [HttpPut("{deptId}")]
        public async Task<IActionResult> UpdateDepartment(int deptId, [FromBody] Department department)
        {
            if (department == null)
            {
                return BadRequest(new { message = "Invalid department data." });
            }

            try
            {
                await _departmentService.UpdateDepartmentAsync(deptId, department);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE: api/Department/{deptId}
        [HttpDelete("{deptId}")]
        public async Task<IActionResult> DeleteDepartment(int deptId)
        {
            try
            {
                await _departmentService.DeleteDepartmentAsync(deptId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }

}

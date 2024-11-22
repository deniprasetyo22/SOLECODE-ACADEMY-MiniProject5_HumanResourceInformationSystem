using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject5.Application.DTOs;
using MiniProject5.Application.Interfaces.IServices;
using MiniProject5.Application.Services;
using MiniProject5.Persistence.Models;

namespace MiniProject5.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // Get all employees with pagination
        [HttpGet]
        public async Task<ActionResult<object>> GetAllEmployees([FromQuery] QueryObject query)
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync(query);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("NoPages")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployeesNoPages()
        {
            var employees = await _employeeService.GetAllEmployeesNoPagesAsync();
            return Ok(employees);
        }

        // Get employee by ID
        [HttpGet("{empId}")]
        public async Task<ActionResult<Employee>> GetEmployee(int empId)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(empId);
                if (employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Add a new employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {try
            {
                var newEmployee = await _employeeService.AddEmployeeAsync(employee);
                return Ok("Employee created successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // 409 Conflict
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update an existing employee
        [HttpPut("{empId}")]
        public async Task<IActionResult> UpdateEmployee(int empId, [FromBody] Employee employee)
        {
            try
            {
                await _employeeService.UpdateEmployeeAsync(empId, employee);
                return Ok("Employee updated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); 
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Deactivate an employee
        [HttpPut("deactivate/{empId}")]
        public async Task<IActionResult> DeactivateEmployee(int empId, [FromBody] string reason)
        {
            try
            {
                await _employeeService.DeactivateEmployeeAsync(empId, reason);
                return Ok("Employee deactived successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("activate/{empId}")]
        public async Task<IActionResult> ActivateEmployee(int empId)
        {
            try
            {
                await _employeeService.ActivateEmployeeAsync(empId);
                return Ok("Employee activate successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // Delete an employee
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return Ok("Employee deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Search for employees
        [HttpGet("search")]
        public async Task<ActionResult<object>> SearchEmployee([FromQuery] searchDto search, [FromQuery] paginationDto pagination)
        {
            if (pagination == null || pagination.pageNumber <= 0 || pagination.pageSize <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than zero.");
            }

            try
            {
                var employees = await _employeeService.SearchEmployee(search, pagination);

                if (employees == null || !employees.Any())
                {
                    return NotFound("No employees found matching the search criteria.");
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}

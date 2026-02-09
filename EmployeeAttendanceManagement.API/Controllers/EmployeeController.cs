using EmployeeAttendanceManagement.Application.DTOs;
using EmployeeAttendanceManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAttendanceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeController(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.GetAllAsync());
        }


        [HttpPost("add")]
        public async Task<IActionResult> Create(EmployeeDTO employeeDTO)
        {
            await _repo.AddEmployeeAsync(employeeDTO);
            return Ok(new { message = "Employee added successfully" });
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDTO  employeeDTO)
        {
            if (id != employeeDTO.EmployeeId)
            {
                return BadRequest("EmployeeId mismatch");
            }


            await _repo.UpdateEmployeeAsync(employeeDTO);

            return Ok(new
            {
                success = true,
                message = "Employee updated successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _repo.SoftDeleteAsync(id);

            return Ok(new
            {
                success = true,
                message = "Employee deleted successfully"
            });
        }


    }
}

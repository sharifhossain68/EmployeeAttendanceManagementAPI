using EmployeeAttendanceManagement.Application.DTOs;
using EmployeeAttendanceManagement.Application.Interfaces;
using EmployeeAttendanceManagement.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAttendanceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employeeService.GetAllAsync());
        }


        [HttpPost("add")]
        public async Task<IActionResult> Create(EmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            
            await _employeeService.AddEmployeeAsync(employeeDTO);
            return Ok(new { message = "Employee added successfully" });
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDTO  employeeDTO)
        {
            if (id != employeeDTO.EmployeeId)
            {
                return BadRequest("EmployeeId mismatch");
            }


            await _employeeService.UpdateEmployeeAsync(employeeDTO);

            return Ok(new
            {
                success = true,
                message = "Employee updated successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.SoftDeleteAsync(id);

            return Ok(new
            {
                success = true,
                message = "Employee deleted successfully"
            });
        }


    }
}

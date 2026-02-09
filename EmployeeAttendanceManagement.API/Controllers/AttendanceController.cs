using EmployeeAttendanceManagement.Application.DTOs;
using EmployeeAttendanceManagement.Application.Interfaces;
using EmployeeAttendanceManagement.Application.Service;
using EmployeeAttendanceManagement.Domain.Entities;
using EmployeeAttendanceManagement.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendanceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {

        private readonly AttendanceService _attendanceService;

        public AttendanceController(AttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
 
      
        [HttpPost("check-in")]
        public async Task<IActionResult> CheckIn(
            AttendanceCheckInDTO  attendanceCheckInDTO)
        {
            await _attendanceService.CheckInAsync(attendanceCheckInDTO);
            return Ok(new
            {
                success = true,
                message = "Check-in successful"
            });
        }

       
        [HttpPost("check-out")]
        public async Task<IActionResult> CheckOut(
            AttendanceCheckOutDTO  attendanceCheckOutDTO)
        {
            await _attendanceService.CheckOutAsync(attendanceCheckOutDTO);
            return Ok(new
            {
                success = true,
                message = "Check-out successful"
            });
        }
      
           

        [HttpPut("edit")]
        public async Task<IActionResult> EditAttendance(AttendanceDTO  attendance)
        {
            try
            {
                await _attendanceService.UpdateAttendanceAsync(attendance);
                return Ok(new { message = "Attendance updated successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            try
            {
                await _attendanceService.DeleteAttendanceAsync(id);
                return Ok(new { message = "Attendance deleted permanently" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpGet("report")]
        public async Task<IActionResult> Report(DateOnly from, DateOnly to)
        {

           return Ok(await _attendanceService.GetReportAsync(from, to));
        }


    }
}

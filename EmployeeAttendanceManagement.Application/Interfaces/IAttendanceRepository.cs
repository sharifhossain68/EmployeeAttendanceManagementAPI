using EmployeeAttendanceManagement.Application.DTOs;
using EmployeeAttendanceManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAttendanceManagement.Application.Interfaces
{
    public interface IAttendanceRepository
    {
        
        Task CheckInAsync(AttendanceCheckInDTO attendanceCheckInDTO);
        Task CheckOutAsync(AttendanceCheckOutDTO attendanceCheckOutDTO);
        Task UpdateAttendanceAsync(AttendanceDTO att);
        Task DeleteAttendanceAsync(int attendanceId);
        Task<List<Attendance>> Report(DateOnly from, DateOnly to);
       
    }

}

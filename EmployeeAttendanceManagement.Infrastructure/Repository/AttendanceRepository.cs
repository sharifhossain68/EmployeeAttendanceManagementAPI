using EmployeeAttendanceManagement.Application.DTOs;
using EmployeeAttendanceManagement.Application.Interfaces;
using EmployeeAttendanceManagement.Domain.Entities;
using EmployeeAttendanceManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendanceManagement.Infrastructure.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {

        private readonly EmployeeAttendanceDbContext _context;

        public AttendanceRepository(EmployeeAttendanceDbContext context)
        {
            _context = context;
        }

        public async Task CheckInAsync(AttendanceCheckInDTO dto)
        {
          
                var checkInTime = dto.CheckInTime;
                var attendanceDate = checkInTime.Date;

                bool exists = await _context.Attendance.AnyAsync(a =>
                    a.EmployeeId == dto.EmployeeId &&
                    a.AttendanceDate == DateOnly.FromDateTime(attendanceDate)
                );

                if (exists)
                    throw new InvalidOperationException(
                        "Attendance already checked in for today"
                    );

                var attendance = new Attendance
                {
                    EmployeeId = dto.EmployeeId,
                    AttendanceDate = DateOnly.FromDateTime(attendanceDate),
                    CheckInTime = checkInTime
                };

                _context.Attendance.Add(attendance);
                await _context.SaveChangesAsync();
            

        }

    
        public async Task CheckOutAsync(AttendanceCheckOutDTO attendanceCheckOutDTO)
        {
            var today = DateOnly.FromDateTime(attendanceCheckOutDTO.CheckOutTime.Date);

            var attendance = await _context.Attendance
                .FirstOrDefaultAsync(a =>
                    a.EmployeeId == attendanceCheckOutDTO.EmployeeId &&
                    a.AttendanceDate == today
                );

            if (attendance == null)
                throw new InvalidOperationException(
                    "Check-in not found for today"
                );

            if (attendance.CheckOutTime != null)
                throw new InvalidOperationException(
                    "Already checked out"
                );

            attendance.CheckOutTime = attendanceCheckOutDTO.CheckOutTime;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAttendanceAsync(AttendanceDTO att)
        {
            var attendance = await _context.Attendance
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.AttendanceId == att.AttendanceId && !a.Employee.IsDeleted);

            if (attendance == null)
                throw new InvalidOperationException("Attendance not found or deleted");

            // Check if the employee already has attendance on the new date
            bool exists = await _context.Attendance
                .AnyAsync(a =>
                    a.AttendanceId != att.AttendanceId &&
                    a.EmployeeId == attendance.EmployeeId &&
                    a.AttendanceDate == att.AttendanceDate &&
                    !a.Employee.IsDeleted
                );

            if (exists)
                throw new InvalidOperationException("Attendance already exists for this date");

            // Update 
            attendance.AttendanceDate = att.AttendanceDate;
            attendance.CheckInTime = att.CheckInTime;
            attendance.CheckOutTime = att.CheckOutTime;
          


            await _context.SaveChangesAsync();
        }
        public async Task DeleteAttendanceAsync(int attendanceId)
        {
            var attendance = await _context.Attendance
                .FirstOrDefaultAsync(a => a.AttendanceId == attendanceId);

            if (attendance == null)
                throw new InvalidOperationException("Attendance not found");

            _context.Attendance.Remove(attendance);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Attendance>> Report(DateOnly from, DateOnly to)
        {
            return await _context.Attendance
                .Include(a => a.Employee)
                .Where(a => a.AttendanceDate >= from && a.AttendanceDate <= to && !a.Employee.IsDeleted )
                .ToListAsync();
        }
     



    }
}

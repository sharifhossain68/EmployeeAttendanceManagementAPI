using EmployeeAttendanceManagement.Application.DTOs;
using EmployeeAttendanceManagement.Application.Interfaces;
using EmployeeAttendanceManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAttendanceManagement.Application.Service
{
   public class AttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task CheckInAsync(AttendanceCheckInDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            await _attendanceRepository.CheckInAsync(dto);
        }

        public async Task CheckOutAsync(AttendanceCheckOutDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            await _attendanceRepository.CheckOutAsync(dto);
        }

        public async Task UpdateAttendanceAsync(AttendanceDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            await _attendanceRepository.UpdateAttendanceAsync(dto);
        }

        public async Task DeleteAttendanceAsync(int attendanceId)
        {
            if (attendanceId <= 0)
                throw new ArgumentException("Invalid Attendance Id");

            await _attendanceRepository.DeleteAttendanceAsync(attendanceId);
        }

        public async Task<List<Attendance>> GetReportAsync(DateOnly from, DateOnly to)
        {
            if (from > to)
                throw new ArgumentException("From date cannot be greater than To date");

            return await _attendanceRepository.Report(from, to);
        }
    }
}

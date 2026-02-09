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
   public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task AddEmployeeAsync(EmployeeDTO employeeDTO)
        {
            if (employeeDTO == null)
                throw new ArgumentNullException(nameof(employeeDTO));

            await _employeeRepository.AddEmployeeAsync(employeeDTO);
        }

        public async Task UpdateEmployeeAsync(EmployeeDTO employeeDTO)
        {
            if (employeeDTO == null || employeeDTO.EmployeeId <= 0)
                throw new ArgumentException("Invalid employee data");

            await _employeeRepository.UpdateEmployeeAsync(employeeDTO);
        }

        public async Task SoftDeleteAsync(int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Invalid Employee Id");

            await _employeeRepository.SoftDeleteAsync(employeeId);
        }
    }
}

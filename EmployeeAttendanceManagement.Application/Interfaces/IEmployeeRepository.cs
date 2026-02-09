using EmployeeAttendanceManagement.Application.DTOs;
using EmployeeAttendanceManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAttendanceManagement.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
        Task AddEmployeeAsync(EmployeeDTO employeeDTO);
        Task UpdateEmployeeAsync(EmployeeDTO employeeDTO);
        Task SoftDeleteAsync(int employeeId);
      
    }

}

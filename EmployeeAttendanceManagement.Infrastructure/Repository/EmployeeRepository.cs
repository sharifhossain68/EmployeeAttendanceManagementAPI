using EmployeeAttendanceManagement.Application.DTOs;
using EmployeeAttendanceManagement.Application.Interfaces;
using EmployeeAttendanceManagement.Domain.Entities;
using EmployeeAttendanceManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAttendanceManagement.Infrastructure.Repository
{
   public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeAttendanceDbContext _context;

        public EmployeeRepository(EmployeeAttendanceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Where(e => !e.IsDeleted)
                .Select(e => new Employee
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeCode =e.EmployeeCode,
                    IsDeleted = e.IsDeleted,
                    FullName = e.FullName,
                    Email = e.Email
                }).ToListAsync();
        }
        public async Task AddEmployeeAsync(EmployeeDTO  employeeDTO)
        {
            try
            {


                var lastEmployee = await _context.Employees
            .OrderByDescending(e => e.EmployeeId)
            .FirstOrDefaultAsync();

                int empnextId = lastEmployee == null ? 1 : lastEmployee.EmployeeId + 1;

                string employeeCode = $"EMP{empnextId:D4}"; 
                var emp = new Employee
                {
                    EmployeeCode = employeeCode,
                    FullName = employeeDTO.FullName,
                    Email = employeeDTO.Email,
                    CreatedAt =DateTime.Now,
                    
                };
                _context.Employees.Add(emp);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Added: {ex.Message}");
            }
        }

        public async Task UpdateEmployeeAsync(EmployeeDTO  employeeDTO)
        {
            var emp = await _context.Employees.FindAsync(employeeDTO.EmployeeId);
            if (emp == null) throw new Exception("Employee not found");

            emp.FullName = employeeDTO.FullName;
            emp.Email = employeeDTO.Email;

            //emp.EmployeeCode = employeeDTO.EmployeeCode;


            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int employeeId)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e =>
                    e.EmployeeId == employeeId &&
                    !e.IsDeleted
                );

            if (employee == null)
                throw new InvalidOperationException(
                    "Employee not found or already deleted"
                );

            employee.IsDeleted = true;

            await _context.SaveChangesAsync();
        }





    }
}

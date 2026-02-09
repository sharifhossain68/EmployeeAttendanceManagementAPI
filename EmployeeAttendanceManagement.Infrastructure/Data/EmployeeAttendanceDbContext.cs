using EmployeeAttendanceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAttendanceManagement.Infrastructure.Data
{
     public class EmployeeAttendanceDbContext: DbContext
    {
        public EmployeeAttendanceDbContext(DbContextOptions<EmployeeAttendanceDbContext> options)
       : base(options) { }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Attendance> Attendance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>()
               .HasIndex(e => e.EmployeeCode)
               .IsUnique();
            //.HasQueryFilter(e => !e.IsDeleted);
            //modelBuilder.Entity<Employee>()
            //.Property(e => e.UpdatedAt)
            //.HasColumnName("UpdatedAt");
            // Attendance unique per day per employee
            modelBuilder.Entity<Attendance>()
                .HasIndex(a => new { a.EmployeeId, a.AttendanceDate })
                .IsUnique();
            //    modelBuilder.Entity<Employee>()
 
            base.OnModelCreating(modelBuilder);
           
        }
    }
}

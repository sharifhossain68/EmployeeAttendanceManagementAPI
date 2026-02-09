using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAttendanceManagement.Domain.Entities
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string EmployeeCode { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }

        //public virtual ICollection<Attendance> Attendances { get; set; }

    }

}

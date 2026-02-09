using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAttendanceManagement.Application.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        //public bool IsDeleted { get; set; }
        //public string EmployeeCode { get; set; }
       
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAttendanceManagement.Application.DTOs
{
    public class AttendanceCheckOutDTO
    {

        public int EmployeeId { get; set; }
        public DateTime CheckOutTime { get; set; }
    }
}

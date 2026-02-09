using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAttendanceManagement.Application.DTOs
{
    public class AttendanceCheckInDTO
    {
        public int EmployeeId { get; set; }
        //public DateOnly AttendanceDate { get; set; }
        public DateTime CheckInTime { get; set; }
       
    }

}

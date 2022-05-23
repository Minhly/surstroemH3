using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class EmployeeShiftsDto
    {
        public int Id { get; set; }
        public DateTime ShiftDate { get; set; }
        public EmployeeDto Employee { get; set; }
        public ShiftInfoDto Shift { get; set; }

    }
}

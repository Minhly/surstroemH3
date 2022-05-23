using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class EmployeeHasShift
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ShiftsId { get; set; }
        public DateTime Date { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Shift Shifts { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class Shift
    {
        public Shift()
        {
            EmployeeHasShifts = new HashSet<EmployeeHasShift>();
        }

        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }

        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<EmployeeHasShift> EmployeeHasShifts { get; set; }
    }
}

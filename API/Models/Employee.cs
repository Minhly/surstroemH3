using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeHasShifts = new HashSet<EmployeeHasShift>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int WarehouseId { get; set; }
        public int? WorkPhone { get; set; }

        public virtual Warehouse Warehouse { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<EmployeeHasShift> EmployeeHasShifts { get; set; }
    }
}

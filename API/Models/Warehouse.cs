using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            Shifts = new HashSet<Shift>();
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public int AddressId { get; set; }
        public int WarehouseTypeId { get; set; }

        public virtual Address Address { get; set; }
        public virtual WarehouseType WarehouseType { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}

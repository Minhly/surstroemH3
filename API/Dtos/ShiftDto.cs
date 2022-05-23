using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ShiftDto
    {
        public int Id { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }

        public WarehouseDto Warehouse { get; set; }

        public ShiftDto()
        {
                
        }

        public ShiftDto(Shift shift)
        {
            Id = shift.Id;
            ShiftStart = shift.ShiftStart;
            ShiftEnd = shift.ShiftEnd;
            Warehouse = new WarehouseDto(shift.Warehouse);
        }
    }
}

using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class WarehouseDto
    {
        public int Id { get; set; }

        public WarehouseTypeDto WarehouseType { get; set; }
        public AddressDto Address { get; set; }

        public WarehouseDto()
        {

        }

        public WarehouseDto(Warehouse warehouse)
        {
            Id = warehouse.Id;
            WarehouseType = new WarehouseTypeDto(warehouse.WarehouseType);
            Address = new AddressDto(warehouse.Address);
        }
    }
}

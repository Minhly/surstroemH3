using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class WarehouseTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public WarehouseTypeDto()
        {

        }

        public WarehouseTypeDto(WarehouseType warehouseType)
        {
            Id = warehouseType.Id;
            Type = warehouseType.Type;
        }
    }
}

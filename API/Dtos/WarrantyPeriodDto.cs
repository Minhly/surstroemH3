using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class WarrantyPeriodDto
    {
        public int Id { get; set; }
        public string WarrantyType { get; set; }
        public double WarrantyPeriod { get; set; }
    }
}

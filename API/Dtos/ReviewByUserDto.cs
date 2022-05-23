using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ReviewByUserDto
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public double Star { get; set; }
        public string Comment { get; set; }
    }
}

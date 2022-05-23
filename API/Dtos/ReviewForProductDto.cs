using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ReviewForProductDto
    {
        public int Id { get; set; }
        public string User { get; set; }
        public double Star { get; set; }
        public string Comment { get; set; }
    }
}

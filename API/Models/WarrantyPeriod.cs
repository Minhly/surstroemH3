using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class WarrantyPeriod
    {
        public WarrantyPeriod()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string WarrantyType { get; set; }
        public double WarrantyPeriod1 { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

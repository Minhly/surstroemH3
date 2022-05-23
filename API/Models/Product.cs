using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string ProductTitle { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double? Weight { get; set; }
        public double? Width { get; set; }
        public double? Length { get; set; }
        public double? Height { get; set; }
        public int? WarrantyPeriodId { get; set; }
        public int? ColorId { get; set; }
        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Color Color { get; set; }
        public virtual WarrantyPeriod WarrantyPeriod { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}

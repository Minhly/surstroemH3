using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class OrderProduct
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductsId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}

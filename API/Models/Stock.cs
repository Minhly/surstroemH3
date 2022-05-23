using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class Stock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}

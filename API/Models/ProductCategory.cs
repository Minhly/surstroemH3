using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class ProductCategory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Product { get; set; }
    }
}

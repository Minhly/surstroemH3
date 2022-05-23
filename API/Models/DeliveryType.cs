using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class DeliveryType
    {
        public DeliveryType()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

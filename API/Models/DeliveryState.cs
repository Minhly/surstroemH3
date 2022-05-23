using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class DeliveryState
    {
        public DeliveryState()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string State { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

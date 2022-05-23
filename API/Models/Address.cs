using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class Address
    {
        public Address()
        {
            Users = new HashSet<User>();
            Warehouses = new HashSet<Warehouse>();
        }

        public int Id { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public int PostalCodeId { get; set; }
        public string Floor { get; set; }
        public string Additional { get; set; }

        public virtual PostalCode PostalCode { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; }
        public virtual ICollection<Order> PayingOrders { get; set; }
        public virtual ICollection<Order> ShippingOrders { get; set; }
    }
}

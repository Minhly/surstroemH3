using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class PostalCode
    {
        public PostalCode()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string PostalCode1 { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}

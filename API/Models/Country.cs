using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class Country
    {
        public Country()
        {
            PostalCodes = new HashSet<PostalCode>();
        }

        public int Id { get; set; }
        public string Country1 { get; set; }

        public virtual ICollection<PostalCode> PostalCodes { get; set; }
    }
}

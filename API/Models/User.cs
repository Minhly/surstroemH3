using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class User
    {   
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int? PhoneNumber { get; set; }
        public int? AddressId { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public virtual Address Address { get; set; }
    }
}

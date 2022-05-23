using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class UserContactInfoDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }

        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string Floor { get; set; }
        public string Additional { get; set; }

        public string PostalCode { get; set; }
        public string CityName { get; set; }

        public string Country { get; set; }
    }
}
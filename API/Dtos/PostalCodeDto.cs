using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PostalCodeDto
    {
        public int Id { get; set; }
        public string Postal { get; set; }
        public string City { get; set; }

        public CountryDto Country { get; set; }


        public PostalCodeDto() { }

        public PostalCodeDto (PostalCode postalCode) 
        {
            Id = postalCode.Id;
            Postal = postalCode.PostalCode1;
            City = postalCode.CityName;
            Country = new CountryDto(postalCode.Country);
        }
    }
}

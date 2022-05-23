using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Country { get; set; }

        public CountryDto() { }

        public CountryDto(Country country) 
        {
            Id = country.Id;
            Country = country.Country1;
        }
    }
}

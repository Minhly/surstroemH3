using Microsoft.AspNetCore.Mvc;
using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string Floor { get; set; }
        public string Additional { get; set; }

        public PostalCodeDto PostalCode { get; set; }

        public AddressDto()
        {

        }

        public AddressDto(Address address)
        {
            Id = address.Id;
            StreetName = address.StreetName;
            HouseNumber = address.HouseNumber;
            Floor = address.Floor;
            Additional = address.Additional;
            PostalCode = new PostalCodeDto(address.PostalCode);
        }
    }
}

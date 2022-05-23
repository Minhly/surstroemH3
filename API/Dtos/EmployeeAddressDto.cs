using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class EmployeeAddressDto
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string Floor { get; set; }
        public string Additional { get; set; }

        public PostalCodeDto PostalCodeInfo { get; set; }

        public EmployeeAddressDto()
        {

        }

        public EmployeeAddressDto(Employee employee)
        {
            Id = (int)employee.User.AddressId;
            StreetName = employee.User.Address.StreetName;
            HouseNumber = employee.User.Address.HouseNumber;
            Floor = employee.User.Address.Floor;
            Additional = employee.User.Address.Additional;
        }
    }
}

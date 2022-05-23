using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class UserBasicInfoDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }

        public AddressDto Address { get; set; }

        public UserBasicInfoDto()
        {

        }

        public UserBasicInfoDto(User user)
        {
            Id = user.Id;
            FirstName = user.Firstname;
            LastName = user.Lastname;
            Email = user.Email;
            PhoneNumber = (int)user.PhoneNumber;
        }
    }
}

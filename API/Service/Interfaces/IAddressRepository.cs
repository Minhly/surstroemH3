using API.Service.Interfaces;
using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service.Interfaces
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<ICollection<Address>> GetAddressWithAllInfo();
    }
}

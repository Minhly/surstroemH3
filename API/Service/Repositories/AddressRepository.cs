using API.Service.Interfaces;
using API.Service.Repositories;
using Microsoft.EntityFrameworkCore;
using surstroem.Data;
using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service.Repositories
{
    public class AddressRepository : GenericRepository<Address, surstroemContext>, IAddressRepository
    {
        public AddressRepository(surstroemContext dbcontext)
            : base(dbcontext)
        {

        }

        public async Task<ICollection<Address>> GetAddressWithAllInfo()
        {
            return await _dbcontext.Set<Address>()
                    .Include(q => q.PostalCode)
                    .Include(e => e.PostalCode.Country)
                    .ToListAsync();
        }
    }
}

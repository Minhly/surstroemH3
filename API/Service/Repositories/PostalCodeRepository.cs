using API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using surstroem.Data;
using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service.Repositories
{
    public class PostalCodeRepository : GenericRepository<PostalCode, surstroemContext>, IPostalCodeRepository
    {
        public PostalCodeRepository(surstroemContext dbcontext)
            : base(dbcontext)
        {

        }

        public async Task<ICollection<PostalCode>> GetPostalsWithAllInfo()
        {
            return await _dbcontext.Set<PostalCode>()
                    .Include(q => q.Country)
                    .ToListAsync();
        }
    }
}

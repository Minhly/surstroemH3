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
    public class WarehouseRepository : GenericRepository<Warehouse, surstroemContext>, IWarehouseRepository
    {
        public WarehouseRepository(surstroemContext dbcontext)
            : base(dbcontext)
        {

        }

        public async Task<ICollection<Warehouse>> GetWarehouseFullInfo()
        {
            return await _dbcontext.Set<Warehouse>()
                    .Include(f => f.Address)
                    .Include(w => w.Address.PostalCode)
                    .Include(s => s.Address.PostalCode.Country)
                    .Include(a => a.WarehouseType)
                    .ToListAsync();
        }

        public async Task<Warehouse> GetWarehouseFullInfoById(int id)
        {
            return await _dbcontext.Warehouses.Where(c => c.Id == id)
                    .Include(f => f.Address)
                    .Include(w => w.Address.PostalCode)
                    .Include(s => s.Address.PostalCode.Country)
                    .Include(a => a.WarehouseType)
                    .FirstOrDefaultAsync();
        }
    }
}

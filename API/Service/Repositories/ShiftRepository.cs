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
    public class ShiftRepository : GenericRepository<Shift, surstroemContext>, IShiftRepository
    {
        public ShiftRepository(surstroemContext dbcontext)
            : base(dbcontext)
        {

        }

        public async Task<ICollection<Shift>> GetAllShiftsByWarehouseId(int warehouseId)
        {
            return await _dbcontext.Shifts.Where(c => c.WarehouseId == warehouseId)
                        .Include(e => e.Warehouse)
                        .Include(e => e.Warehouse.WarehouseType)
                        .Include(s => s.Warehouse.Address)
                        .Include(s => s.Warehouse.Address.PostalCode)
                        .Include(s => s.Warehouse.Address.PostalCode.Country)
                        .ToListAsync();
        }
    }
}

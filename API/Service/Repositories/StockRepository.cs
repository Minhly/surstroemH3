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
    public class StockRepository : GenericRepository<Stock, surstroemContext>, IStockRepository
    {
        public StockRepository(surstroemContext dbcontext)
            : base(dbcontext)
        {

        }

        public async Task<ICollection<Stock>> GetProductByWarehouseId(int warehouseId)
        {
            return await _dbcontext.Stocks.Where(c => c.WarehouseId == warehouseId)
                .Include(c => c.Warehouse)
                .Include(c => c.Warehouse.WarehouseType)
                .Include(c => c.Warehouse.Address)
                .Include(c => c.Warehouse.Address.PostalCode)
                .Include(c => c.Warehouse.Address.PostalCode.Country)
                .ToListAsync();
        }

        public async Task<ICollection<Stock>> GetStockQuantityByProductId(int productId)
        {
            return await _dbcontext.Stocks.Where(c => c.ProductId == productId)
                .Include(c => c.Warehouse)
                .Include(c => c.Warehouse.WarehouseType)
                .Include(c => c.Warehouse.Address)
                .Include(c => c.Warehouse.Address.PostalCode)
                .Include(c => c.Warehouse.Address.PostalCode.Country)
                .ToListAsync();
        }
    }
}

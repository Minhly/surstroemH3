using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service.Interfaces
{
    public interface IStockRepository : IGenericRepository<Stock>
    {
        Task<ICollection<Stock>> GetStockQuantityByProductId(int productId);
        Task<ICollection<Stock>> GetProductByWarehouseId(int warehouseId);
    }
}

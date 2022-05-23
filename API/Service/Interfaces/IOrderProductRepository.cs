using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service.Interfaces
{
    public interface IOrderProductRepository : IGenericRepository<OrderProduct>
    {
        Task<ICollection<OrderProduct>> GetOrderProductsByOrderId(int orderId);
        Task<ICollection<OrderProduct>> GetOrdersByProductId(int productId);
    }
}

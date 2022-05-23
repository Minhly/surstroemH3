using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<ICollection<Order>> GetOrdersFromUserId(int userId);
        Task<ICollection<Order>> GetOrdersWithAllInfo();
    }
}

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
    public class OrderProductRepository : GenericRepository<OrderProduct, surstroemContext>, IOrderProductRepository
    {
        public OrderProductRepository(surstroemContext dbcontext)
            : base(dbcontext)
        {

        }

        public async Task<ICollection<OrderProduct>> GetOrdersByProductId(int productId)
        {
            return await _dbcontext.OrderProducts.Where(c => c.ProductsId == productId)
                .Include(q => q.Order)
                .Include(q => q.Order.User)
                .Include(q => q.Order.User.Address)
                .Include(q => q.Order.User.Address.PostalCode)
                .Include(q => q.Order.User.Address.PostalCode.Country)
                .Include(q => q.Order.PayingAddress)
                .Include(q => q.Order.PayingAddress.PostalCode)
                .Include(q => q.Order.PayingAddress.PostalCode.Country)
                .Include(q => q.Order.ShippingAddress)
                .Include(q => q.Order.ShippingAddress.PostalCode)
                .Include(q => q.Order.ShippingAddress.PostalCode.Country)
                .Include(q => q.Order.DeliveryState)
                .Include(q => q.Order.DeliveryType)
                .ToListAsync();
        }

        public async Task<ICollection<OrderProduct>> GetOrderProductsByOrderId(int orderId)
        {
            return await _dbcontext.OrderProducts.Where(c => c.OrderId == orderId)
                .ToListAsync();
        }
    }
}

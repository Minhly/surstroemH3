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
    public class OrderRepository : GenericRepository<Order, surstroemContext>, IOrderRepository
    {
        public OrderRepository(surstroemContext dbcontext)
            : base(dbcontext)
        {

        }

        public async Task<ICollection<Order>> GetOrdersFromUserId(int userId)
        {
            return await _dbcontext.Orders.Where(c => c.UserId == userId)
                .Include(q => q.ShippingAddress)
                .Include(q => q.ShippingAddress.PostalCode)
                .Include(q => q.ShippingAddress.PostalCode.Country)
                .Include(w => w.PayingAddress)
                .Include(w => w.PayingAddress.PostalCode)
                .Include(w => w.PayingAddress.PostalCode.Country)
                .Include(e => e.User)
                .Include(e => e.User.Address)
                .Include(e => e.User.Address.PostalCode)
                .Include(e => e.User.Address.PostalCode.Country)
                .Include(r => r.DeliveryState)
                .Include(t => t.DeliveryType)
                .ToListAsync();
        }

        public async Task<ICollection<Order>> GetOrdersWithAllInfo()
        {
            return await _dbcontext.Set<Order>()
                .Include(q => q.ShippingAddress)
                .Include(q => q.ShippingAddress.PostalCode)
                .Include(q => q.ShippingAddress.PostalCode.Country)
                .Include(w => w.PayingAddress)
                .Include(w => w.PayingAddress.PostalCode)
                .Include(w => w.PayingAddress.PostalCode.Country)
                .Include(e => e.User)
                .Include(e => e.User.Address)
                .Include(e => e.User.Address.PostalCode)
                .Include(e => e.User.Address.PostalCode.Country)
                .Include(r => r.DeliveryState)
                .Include(t => t.DeliveryType)
                .ToListAsync();
        }
    }
}

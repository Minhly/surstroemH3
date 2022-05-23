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
    public class DeliveryTypeRepository : GenericRepository<DeliveryType, surstroemContext>, IDeliveryTypeRepository
    {
        public DeliveryTypeRepository(surstroemContext dbcontext)
            : base(dbcontext)
        {

        }
    }
}
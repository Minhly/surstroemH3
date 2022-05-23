using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service.Interfaces
{
    public interface IPostalCodeRepository : IGenericRepository<PostalCode>
    {
        Task<ICollection<PostalCode>> GetPostalsWithAllInfo();
    }
}

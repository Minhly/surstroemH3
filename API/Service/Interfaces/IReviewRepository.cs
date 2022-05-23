using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<ICollection<Review>> GetReviewsByProductId(int productId);
        Task<ICollection<Review>> GetReviewsByUserId(int userId);
    }
}

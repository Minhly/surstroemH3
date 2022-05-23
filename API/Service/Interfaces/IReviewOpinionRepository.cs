using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service.Interfaces
{
    public interface IReviewOpinionRepository : IGenericRepository<ReviewOpinion>
    {
        Task<int> GetROByUserIdAndReviewId(int userId, int reviewId);
        Task<ICollection<ReviewOpinion>> GetReviewOpinionsByReviewId(int reviewId);
    }
}

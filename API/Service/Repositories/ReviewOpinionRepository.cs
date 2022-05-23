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
    public class ReviewOpinionRepository : GenericRepository<ReviewOpinion, surstroemContext>, IReviewOpinionRepository
    {
        public ReviewOpinionRepository(surstroemContext dbcontext)
            : base(dbcontext)
        {

        }

        public async Task<ICollection<ReviewOpinion>> GetReviewOpinionsByReviewId(int reviewId)
        {
            return await _dbcontext.ReviewOpinions.Where(c => c.ReviewId == reviewId)
                .ToListAsync();
        }

        public async Task<int> GetROByUserIdAndReviewId(int userId, int reviewId)
        {
            var result = await _dbcontext.ReviewOpinions.Where(c => c.UserId == userId && c.ReviewId == reviewId).FirstOrDefaultAsync();
            if(result != null) 
            {
                return result.Id;
            }
            else
            {
                return 0;
            }
        }
    }
}
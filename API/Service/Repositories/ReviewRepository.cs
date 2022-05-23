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
    public class ReviewRepository : GenericRepository<Review, surstroemContext>, IReviewRepository
    {
        public ReviewRepository(surstroemContext dbcontext)
            : base(dbcontext)
        {

        }

        public async Task<ICollection<Review>> GetReviewsByProductId(int productId)
        {
            return await _dbcontext.Reviews.Where(c => c.ProductId == productId)
                    .Include(w => w.User)
                    .ToListAsync();
        }

        public async Task<ICollection<Review>> GetReviewsByUserId(int userId)
        {
            return await _dbcontext.Reviews.Where(c => c.UserId == userId)
                    .ToListAsync();
        }
    }
}

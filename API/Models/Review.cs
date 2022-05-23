using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class Review
    {
        public Review()
        {
            ReviewOpinions = new HashSet<ReviewOpinion>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public double Star { get; set; }
        public string Comment { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }

        public virtual ICollection<ReviewOpinion> ReviewOpinions { get; set; }
    }
}

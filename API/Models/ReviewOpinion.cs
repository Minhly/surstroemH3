using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class ReviewOpinion
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ReviewId { get; set; }
        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }

        public virtual Review Review { get; set; }
    }
}

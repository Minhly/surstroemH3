using System;
using System.Collections.Generic;

#nullable disable

namespace surstroem.Models
{
    public partial class Category
    {
        public Category()
        {
            InverseParentCategory = new HashSet<Category>();
            ProductCategories = new HashSet<ProductCategory>();
        }

        public int Id { get; set; }
        public string Category1 { get; set; }
        public int? ParentCategoryId { get; set; }

        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> InverseParentCategory { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}

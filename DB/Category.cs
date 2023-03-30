using System;
using System.Collections.Generic;

#nullable disable

namespace YaProf.DB
{
    public partial class Category
    {
        public Category()
        {
            Articles = new HashSet<Article>();
        }

        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ParentCategory Parent { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}

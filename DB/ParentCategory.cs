using System;
using System.Collections.Generic;

#nullable disable

namespace YaProf.DB
{
    public partial class ParentCategory
    {
        public ParentCategory()
        {
            Categories = new HashSet<Category>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}

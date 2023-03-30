using System;
using System.Collections.Generic;
using YaProf.Models;

#nullable disable

namespace YaProf.DB
{
    public partial class Article
    {
        public Article(MyArticle article)
        {
            Id = article.Id;
            CategoryId = article.Category_Id;
            Name = article.Name;
            Description = article.Description;
        }
        public Article()
        {
            
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Category Category { get; set; }
    }
}

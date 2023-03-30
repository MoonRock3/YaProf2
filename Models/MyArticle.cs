using System;
using YaProf.DB;

namespace YaProf.Models
{
    [Serializable]
    public class MyArticle
    {
        public MyArticle()
        {
            
        }

        public MyArticle(Article article)
        {
            Id = article.Id;
            Category_Id = article.CategoryId;
            Name = article.Name;
            Description = article.Description;
        }

        public int Id { get; set; }
        public int Category_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

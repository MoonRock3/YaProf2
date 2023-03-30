using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using YaProf.DB;
using YaProf.Models;

namespace YaProf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public JsonResult Articles(MyArticle article)
        {
            MyArticle myArticle;
            using (YaProfContext context = new())
            {
                try
                {
                    Article articleToAdd = new(article);
                    var added = context.Articles.Add(articleToAdd).Entity;
                    context.SaveChanges();

                    myArticle = new(added);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Json(ex);
                }
            }
            return Json(myArticle ?? new MyArticle());
        }
        [HttpGet]
        public JsonResult Articles(int id, string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                if (id == 0)
                {
                    List<MyArticle> myArticles = new();
                    List<Article> articles = new();
                    using (YaProfContext context = new())
                    {
                        try
                        {
                            articles.AddRange(context.Articles);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return Json(ex);
                        }
                    }
                    foreach (var item in articles)
                    {
                        myArticles.Add(new MyArticle(item));
                    }
                    return Json(myArticles);
                }
                else
                {
                    MyArticle myArticle;
                    using (YaProfContext context = new())
                    {
                        try
                        {
                            var article = context.Articles.FirstOrDefault(a => a.Id == id);
                            myArticle = new(article);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return Json(ex);
                        }
                    }
                    return Json(myArticle);
                }
            }
            else
            {
                List<Article> articles = new();
                using (YaProfContext context = new())
                {
                    try
                    {
                        articles.AddRange(context.Articles.Where(x => x.Name.Contains(query) 
                        || x.Description.Contains(query) || x.Category.Name.Contains(query)
                        || x.Category.Parent.Name.Contains(query)));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return Json(ex);
                    }
                }
                List<MyArticle> myArticles = new();
                foreach (var item in articles)
                {
                    myArticles.Add(new MyArticle(item));
                }
                return Json(myArticles);
            }
        }
        [HttpPut]
        public JsonResult Articles(int id, MyArticle myArticle)
        {
            Article updated;
            using (YaProfContext context = new())
            {
                try
                {
                    Article article = new(myArticle);
                    updated = context.Articles.Update(article).Entity;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Json(ex);
                }
            }
            return Json(updated);
        }
        [HttpDelete]
        public JsonResult Articles(int id)
        {
            Article deleted;
            using (YaProfContext context = new())
            {
                try
                {
                    Article article = context.Articles.FirstOrDefault(x => x.Id == id);
                    deleted = context.Articles.Remove(article).Entity;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Json(ex);
                }
            }
            return Json(deleted);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Models;
namespace BookShop.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();
        
      public ViewResult Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = db.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление
            return View();
        }
        public ViewResult SomeMethod()
        {
            ViewBag.Head = "Привет мир!";
            return View("SomeView");
        }
        public ActionResult About()
        {
            ViewData["Head"] = "Прив!";
            return View();
        }
        
        [HttpGet]

        public ActionResult Buy(int? id)
        {
            if (id > 3)
            {
                return Redirect("/Home/Index");
            }
            ViewBag.BookId = id ?? 0;
            return View();
        }
        public string Square(int a, int h)
        {
            double s = a * h / 2.0;
            return "<h2>Площадь треугольника с основанием " + a +
                    " и высотой " + h + " равна " + s + "</h2>";
        }
      public FileResult GetFile()
        {
            string file_path = Server.MapPath("~/File/file.docx");
            string file_type = "application/docx";
            string file_name = "file.docx";
            return File(file_path, file_type,file_name);
        }
        public string AboutBrowser()
        {
            string browser = HttpContext.Request.Browser.Browser;
            string user_agent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
            return "<p>Browser: " + browser + "</p><p>User-Agent: " + user_agent + "</p><p>Url запроса: " + url +
                "</p><p>Реферер: " + referrer + "</p><p>IP-адрес: " + ip + "</p>";


        }



            [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо," + purchase.Person + ", за покупку!";
        }
    }
}
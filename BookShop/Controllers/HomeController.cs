using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Models;
using System.Data.Entity;

namespace BookShop.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();
        
      public ViewResult Index()
        {
            ViewBag.Message = "Это вызов частичного представления из обычного";
            ViewData["Head"] = new string[] { "USA", "Canada", "France" };
            // получаем из бд все объекты Book
            IEnumerable<Book> books = db.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление
            return View();
        }
        [HttpPost]
        public string Index(string[] countries)
        {
            string result = "";
            foreach (string c in countries)
            {
                result += c;
                result += ";";
            }
            return "Вы выбрали: " + result;
        }
        [HttpGet]
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.Find(id);
            if (book != null)
            {
                return View(book);
            }
            return HttpNotFound();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            db.Entry(book).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
      [HttpGet]
      public ActionResult Delete(int id)
        {
            Book b = db.Books.Find(id);
            if(b==null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
      [HttpPost, ActionName("Delete")]
      public ActionResult DeleteConfirmed(int id)
        {
            Book b = db.Books.Find(id);
            if(b==null)
            {
                return HttpNotFound();

            }
            db.Books.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BookView(int id)
        {
            Book book = db.Books.Where(p => p.Id == id).FirstOrDefault();
            return View(book);

        }
        public ActionResult Partial()
        {
            ViewBag.Message = "Это частичное представление.";
            return PartialView();
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
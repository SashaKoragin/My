using System;
using System.Collections.Generic;
using System.Linq;
using WebTest.Models;
using System.Web.Mvc;
using WebTest.Util;

namespace WebTest.Controllers
{
    public class HomeController : Controller
    {
        private BookContext Db = new BookContext();
        public ActionResult Index()
        {
            var books = Db.Books;
            ViewBag.Books = books;
           return View();
          //  return new HtmlResult("<h2>Привет мир!<h2>");
        }

        [HttpGet]
        public ActionResult Bye(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public string Bye(Purchase pucture)
        {
            pucture.Date = DateTime.Now;
            Db.Purchases.Add(pucture);
            Db.SaveChanges();
            return "Спасибо, " + pucture.Persone + "за покупку!!!";
        }

        public string GetId(int id)
        {
            return id.ToString();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
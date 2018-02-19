using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTest.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        [HttpGet]
        public ActionResult GetBook()
        {
            return View();
        }

        [HttpPost]
        public string GetBook(string title, string autohor)
        {
            return title + "__" + autohor;
        }

        [HttpPost]
        public string PostName(string title, string autohor)
        {
            return title + "__" + autohor;
        }
    }

}
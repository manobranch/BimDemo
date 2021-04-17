using productuploader.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace productuploader.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var products = BusinessLogic.GetProducts();
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            BusinessLogic.WebRequest();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(FormCollection col, HttpPostedFileBase[] file)
        {
            BusinessLogic.AddProduct(col, file);

            return View();
        }

        public ActionResult CreateError()
        {
            ViewBag.Message = "Create some error";

            BusinessLogic.ThrowException();


            return View();
        }

    }
}
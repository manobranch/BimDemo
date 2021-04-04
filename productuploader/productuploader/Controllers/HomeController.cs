﻿using productuploader.Logic;
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

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
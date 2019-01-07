﻿using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("About");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
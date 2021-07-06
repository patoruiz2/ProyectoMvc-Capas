using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ErrorExController : Controller
    {
        // GET: ErrorEx
        public ActionResult Index(string Error)
        {
            ViewBag.error = Error;
            return View();
        }
    }
}
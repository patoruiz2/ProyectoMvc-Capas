using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Logout()
        {
            Session["Admin"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Log_Out()
        {
            if (Session["Admin"] != null)
            {
                Session["Admin"] = null;
                return RedirectToAction("Index", "Login");
            }
            else if (Session["Student"] != null)
            {
                Session["Student"] = null;
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Login");
        }
    }
}
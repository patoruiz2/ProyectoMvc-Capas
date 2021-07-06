using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.FilterAuth;
using WebApplication1.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using BL;

namespace WebApplication1.Controllers
{
    [AuthorizeUserRol(roles: "admin")]
    public class AdminController : Controller
    {
        private readonly BL.UserBL userBL = new BL.UserBL();
        
        public ActionResult Index(string message)
        {
            @ViewBag.Message = message;
            var model = userBL.List();
            return View(model);
        }
        public ActionResult AddUser(string message)
        {
            ViewBag.Message = message;
            ET.User model = new ET.User();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddUser(ET.User model)
        {
            if(model.fecha < DateTime.Today) 
            { 
                return RedirectToAction("AddUser", new { message = "La fecha es invalida" });
            }
            var validate = userBL.Add(model.nombre, model.password, model.email, model.fecha, model.idRol);
            return RedirectToAction("AddUser", "Admin");
        }
        public ActionResult Delete(int id, string message)
        {
            ViewBag.Message = message;
            userBL.Delete(id);
            return RedirectToAction("Index", new { message = "El usuario se ha eliminado correctamente." });
        }
        public ActionResult Modify(int id)
        {
            var model = userBL.Find(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Modify(ET.User model)
        {

            userBL.Modify(model);

            return RedirectToAction("Index", "Admin");
        }


        public ActionResult Settings()
        {
            ET.User model = new ET.User();
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Settings(ET.User model, string pass, string cPass)
        {
            var context = userBL.ModelToSett(model);
            if(context == true && pass == cPass)
            {
                userBL.ModifyPass(pass, model);
            }
            return View();
        }

        public ActionResult Information()
        {
            ET.User model = new ET.User();
            model = userBL.ModelToInf();

            return View(model);
        }
    }

}
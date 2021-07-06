using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ET;
using BL;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly BL.UserBL userBL = new BL.UserBL();
        

        // GET: Login
        public ActionResult Index(string message)
        {
            ViewBag.Message = message;
            ET.User model = new ET.User();
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(ET.User model)

        {

            var validate = userBL.Login(model.nombre, model.password);
            if(validate == false)
            {
                
                return RedirectToAction("Index", new {message = "Usuario y/o contraseña incorrectos" });
                
            }
            else
            {
               
                var userModel = userBL.Model(model.nombre,model.password);
                if(userModel.idRol == 1)
                {
                    Session["Admin"] = userModel;
                    return RedirectToAction("Index", "Admin");

                }
                else if(userModel.idRol == 2)
                {
                    Session["Student"] = userModel;
                    return RedirectToAction("Index", "Payment");

                }
                
                return View();
            }
            
        }
        public ActionResult Log_Out()
        {
            if (Session["Admin"] != null)
            {
                Session["Admin"] = null;
                return RedirectToAction("Index");
            } else if(Session["Student"] != null)
            {
                Session["Student"] = null;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Login");
        }
    }
}
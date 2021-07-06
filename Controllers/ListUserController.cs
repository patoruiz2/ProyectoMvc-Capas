using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using WebApplication1.Models;
using WebApplication1.database_access;



namespace WebApplication1.Controllers
{
    public class ListUserController : Controller
    {
        // GET: ListUser
        db dbmostrar = new db();
        public ActionResult Show_data()
        {
            DataSet d = dbmostrar.Show_data();
            ViewBag.emp = d.Tables[0];
            return View();
        }

        //public ActionResult Add_user()
        //{

            

        //    return View();
        //}
        [HttpPost]
        public ActionResult Add_user(FormCollection fc)
        {

            Usuario usua = new Usuario();
            //usua.id = int.Parse(fc["ID"]);
            usua.nombre = fc["nombre"];
            usua.email = fc["email"];
            usua.password = fc["Password"];
            usua.fecha = DateTime.Parse(fc["fecha"]);
            usua.idRol =  int.Parse(fc["idRol"]);
            dbmostrar.Add_user(usua);
            TempData["msg"] = "Usuario dado de alta con exito";

            return View();
        }
        //public ActionResult Update_user(int id)
        //{
        //    DataSet ds = dbmostrar.Show_user(id);
        //    ViewBag.emp = ds.Tables[2];
        //    return View();

        //}


        public ActionResult Update_user(int id, FormCollection fc)
        {
            Usuario usua = new Usuario();

            //int.Parse(fc["ID"]);

            usua.id = int.Parse(fc["id"]);
            usua.nombre = fc["nombre"];
            usua.email = fc["email"];
            usua.password = fc["password"];
            usua.fecha = DateTime.Parse(fc["fecha"]);
            usua.idRol = int.Parse(fc["idRol"]);
            dbmostrar.Update_user(usua);
            TempData["msg"] = "Actualizacion con exito";
            return RedirectToAction("Show_data");
        }

        public ActionResult Delete_user(int id)
        {

            dbmostrar.Delete_user(id);
            TempData["msg"] = "Borrado con exito";
            return RedirectToAction("Show_data");
        }
    }
}
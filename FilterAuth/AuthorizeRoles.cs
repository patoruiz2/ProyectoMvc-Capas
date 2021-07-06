using ET;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
//using WebApplication1.Models;

namespace WebApplication1.FilterAuth
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AuthorizeUserRol : AuthorizeAttribute
    {
        private ET.User userA, userS;

        //private MiSistemaEntities db = new MiSistemaEntities();
        private string roles;

        public AuthorizeUserRol(string roles)
        {
            this.roles = roles;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //ET.Rol model = new ET.Rol();
            try
            {

                userA = (ET.User)HttpContext.Current.Session["Admin"];
                if (userA == null)
                {
                    userS = (ET.User)HttpContext.Current.Session["Student"];
                    if (userS == null)
                    {
                        filterContext.Result = new RedirectResult("/Login/Index");
                    }
                    else
                    {

                        string connectionstring = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;


                        SqlConnection c = new SqlConnection();

                        c.ConnectionString = connectionstring;
                        c.Open();
                        SqlCommand cm = new SqlCommand("SELECT * FROM rol WHERE nombre = @nombre", c);
                        cm.CommandType = CommandType.Text;
                        cm.Parameters.AddWithValue("@nombre",userS.Rol.nombre );
                    
                        cm.ExecuteNonQuery();
                        SqlDataReader query = cm.ExecuteReader();
                        query.Read();
                        var rol = query.GetString(1);
                        Console.WriteLine(rol);


                        //var rolName2 = (from rs in db.rol
                        //                where rs.nombre == roles
                        //                && rs.id == userS.idRol
                        //                select rs.nombre).FirstOrDefault();
                        if (rol != roles)
                        {
                            filterContext.Result = new RedirectResult("/ErrorEx/Index");
                        }
                    }

                }
                else
                {
                    string connectionstring = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

                    SqlConnection c = new SqlConnection();

                    c.ConnectionString = connectionstring;
                    c.Open();
                    SqlCommand cm = new SqlCommand("SELECT * FROM rol WHERE nombre = @nombre", c);
                    cm.CommandType = CommandType.Text;

                    cm.Parameters.AddWithValue("@nombre", userA.Rol.nombre);

                    cm.ExecuteNonQuery();
                    SqlDataReader query = cm.ExecuteReader();
                    query.Read();
                    var rol2 = query.GetString(1);
                    Console.WriteLine(rol2);
                    

                    if (rol2 != roles)
                    {

                        filterContext.Result = new RedirectResult("/ErrorEx/Index");
                    }
                }



            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("/ErrorEx/Index ");
            }


        }




        //var encryptCookie = context.HttpContext.Request.Cookies.Get(".ASPXAUTH");
        //var decryptCookie = FormsAuthentication.Decrypt(encryptCookie.Value);



    }
}
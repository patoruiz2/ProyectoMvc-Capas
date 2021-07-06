using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.FilterAuth;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;


namespace WebApplication1.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //PUT- ALTA USUARIOS
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();

        }
    

            [HttpPost]
            public ActionResult Contact( string nom, string email, string pass, DateTime fecha, int idrol )
            
            
            {

                try
                {


                    SqlConnection c = new SqlConnection("Data Source = .\\SQLEXPRESS; Initial Catalog = MiSistema; Integrated Security = True");

                    //string connectionstring = ConfigurationManager.ConnectionStrings["MiSistemaEntities"].ConnectionString;

                    //SqlConnection  c = new SqlConnection();

                    //c.ConnectionString = connectionstring;

                    //using (Models.MiSistemaEntities u = new Models.MiSistemaEntities());

                    

                    SqlCommand cm = new SqlCommand("sp_insertar_user", c);
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add("@nom", SqlDbType.VarChar);
                    cm.Parameters.Add("@email", SqlDbType.VarChar);
                    cm.Parameters.Add("@pass", SqlDbType.VarChar);
                    cm.Parameters.Add("@fecha", SqlDbType.DateTime);
                    cm.Parameters.Add("@idrol", SqlDbType.Int);

                    cm.Parameters["@nom"].Value = nom;
                    cm.Parameters["@email"].Value = email;
               // cm.Parameters["@pass"].Value = pass;

                    cm.Parameters["@pass"].Value = database_access.Encrypt.GetSHA256(pass.ToString());
                    cm.Parameters["@fecha"].Value = fecha;
                    cm.Parameters["@idrol"].Value = idrol;
                    
                    c.Open();

                    cm.ExecuteNonQuery();
                    //mensajes al usuario de confirmacion

                    c.Close();

                return View();
                

                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
}
}
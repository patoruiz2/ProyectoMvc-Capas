using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.FilterAuth;

namespace WebApplication1.Controllers
{
    [AuthorizeUserRol(roles: "student")]
    public class PaymentController : Controller
    {
        private readonly BL.UserBL userBL = new BL.UserBL();

        // GET: Payment
        public ActionResult Index()
        {
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            using (SqlConnection connect = new SqlConnection())
            {
                connect.ConnectionString = connection;

                SqlCommand command = new SqlCommand("select * from Pay", connect);
                command.CommandType = CommandType.Text;

                connect.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<ET.FeeMonth> months = new List<ET.FeeMonth>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ET.FeeMonth month = new ET.FeeMonth()
                        {
                            Id = reader.GetInt32(0),
                            Month = reader.GetString(1)

                        };
                        months.Add(month);

                    }
                }
                ViewBag.Month = months;
            }

            return View();
        }
    
        public ActionResult Settings()
        {
            ET.User model = new ET.User();

            return View(model);
        }

        public ActionResult Information()
        {
            ET.User model = new ET.User();
            model = userBL.ModelToInfS();

            return View(model);
        }
    }
}
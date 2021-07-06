using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.database_access
{
    public partial class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<int> idRol { get; set; }

    }
}
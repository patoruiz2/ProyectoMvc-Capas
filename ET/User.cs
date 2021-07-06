using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ET
{
    public class User
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime fecha { get; set; } 
        public int idRol { get; set; }
        public Rol Rol { get; set; }

        public User()
        {
            Rol = new Rol();
        }
    }
}

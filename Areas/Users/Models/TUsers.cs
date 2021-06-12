using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Areas.Users.Models
{
    public class TUsers
    {
        //referencia propiedades a unas columnas de una tabla a crear
        public int ID { get; set; }//es entera ya que utilizaa el framework para que sepa que esta propiedad tndra la llave primaria y sera autoincrementable
        public string Name { get; set; }
        public string LastName { get; set; } 
        public string NID { get; set; }
        public string Email { get; set; }
        public string IdUser { get; set; }
        public byte[] Image { get; set; }
    }
}

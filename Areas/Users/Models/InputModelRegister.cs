using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Areas.Users.Models
{
    public class InputModelRegister
    {
        //se crean dos propiedades
        //el atributo required resibe como parametro la advertencia de texto mensaje de validacion dependiendo de los datos recibidos como name 

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "El campo Nid es obligatorio.")]
        public string NID { get; set; }


        [Required(ErrorMessage = "El campo Telefono es obligatorio.")]
        [DataType(DataType.PhoneNumber)]//se indica que tipo de informacion se almacena
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{6})$", ErrorMessage ="El formato del telefono ingresado NO es valido.")]
        //se verifica el formato del numero de telefono
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "El campo Correo Electronico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo correo electronico ingresado no es una direccion de correo valida")]
        //para verificar si el campo de texto es un  correo electronico correcto
        public string Email { get; set; }


        [Display(Name ="Contraseña")]// sirve para cambiar el nombre de la propiedad Password para que no se vea visualizar por el lado del cliente
        [Required(ErrorMessage ="El campo Contraseña es obligatorio.")]
        [StringLength(100, ErrorMessage ="El numero de caracteres {0} debe ser almenos de {2}.", MinimumLength = 6)]
        // para indicar la longitud de caracteres a ingresar  primer campo la cantidad de letras. en 0 se muestra el nombre de la propiedad
        //con minimum se indica la cantidad minimaque se ingresa 
        public string Password { get; set; }

        //propiedad para capturar la informacion de los roles

        [Required(ErrorMessage = "El campo del ROL es obligatorio.")]
        public string Role { get; set; }
    }
}

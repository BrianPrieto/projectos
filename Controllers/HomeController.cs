using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SalesSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Controllers
{
    public class HomeController : Controller
    {
        //se crea un objeto de una interface para poder crear unos servicios (solo el codigo comentado es para crear los roles una vez)
        //IServiceProvider _serviceProvider;
        //se le asigna un parametro al objeto de abajo para poder crear una injeccion
        public HomeController(IServiceProvider serviceProvider)
        {
            //y asi se inicializa este metodo dentro del metodo constructor osea este 
            //_serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> Index()//se sincroniza con CreateAsync ya que tiene que esperarlo para poder hacer el registro 
        {
            //dentro del index se llama al metodo y que sea asincronico es decir que este bloquea hasta que recibe una respuesta del servidor
            //await CreateRoleAsync(_serviceProvider);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async Task CreateRoleAsync(IServiceProvider serviceProvider)
        {
            //OBTENER UN SERVICIO DEL METODO PROVICIONAL CREADO obteniendolo del servicio de la clase identitirole por medio de rolemanager clase que nos permitira administrar todos los roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            String[] rolesName = { "Admin", "User" };//areglo inicializado con coleccion de roles a registrar con dos roles
            //ahora se recorrera la coleccion de roles
            foreach (var item in rolesName)
            {
                //se indica al metodo crearroles que tiene q esperar al metodo rolemanager que realice la tarea de verificacion 
                var roleExist = await roleManager.RoleExistsAsync(item);
                if (!roleExist)//si arriba nos devuelte verdadero aqui sera como falso y pasa al contrario
                {
                    //para que se sincrinice con el metodo y tiene que esperarlo hasta que ejecuten su tarea
                    await roleManager.CreateAsync(new IdentityRole(item));
                }
            }
        }
    }
}

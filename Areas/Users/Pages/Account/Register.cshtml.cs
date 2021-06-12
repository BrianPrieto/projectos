using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Areas.Users.Models;
using SalesSystem.Data;
using SalesSystem.Library;

namespace SalesSystem.Areas.Users.Pages.Account
{
    public class RegisterModel : PageModel
    {
        //se crean los objetos de la siguientes clases 
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;
        private LUserRoles _userRoles; //Se llaman a los objetos de las clases 
        private static InputModel _dataInput;
        private Uploadimage _uploadimage;
        private IWebHostEnvironment _environment;

        public RegisterModel(//se pásan como palametros los objetos con sus clases
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            //crear una injeccion
            IWebHostEnvironment environment)

        {
            //se inicializan con un constructor
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _environment = environment;
            _userRoles = new LUserRoles();
            _uploadimage = new Uploadimage();
        }
        public void OnGet()//captura de informacion de paginas 
        {
            if (_dataInput != null)//si es distinto es por que tiene datos y dara error(aqui se evalua)
            {
                Input = _dataInput;//se inicializa para mostrar informacion en el formulario
                Input.rolesLista = _userRoles.getRoles(_roleManager);//llama propiedad y se le aigna informacion 
                Input.AvatarImage = null;
            }
            else
            {
                //input se inicialisa con la clase y se llama a la propiedad 
                Input = new InputModel
                {
                    rolesLista = _userRoles.getRoles(_roleManager)//se pasan lo metodos del constructor PARA OBTENER LOS ROLES
                };
            }
        }
        [BindProperty]

        public InputModel Input { get; set; }//la propiedad input se utilizara para tener acceso a los elementos de la clase mas abajo Input model que ereda desde la interfaz de usuario
        public class InputModel : InputModelRegister
        //ereda de la clase puesta. para tener acceso a sus elementos 
        {
            public IFormFile AvatarImage { get; set; }//propiedad de la interface gestion de archivo utilizando el formulario de registro
            [TempData]//almacenamiento de datos temporales
            public String ErrorMessage { get; set; }

            public List<SelectListItem> rolesLista { get; set; }//contiene coleccion de la clase selec
        }
        //metodo post para captura de informacion por peticion
        public async Task<IActionResult> OnPost()
        {
            //condicion
            if (await SaveAsync())//el metodo tiene que esperar a q se ejecute saveasync osea el procedimiento de registro
            {
                return Redirect("/Users/Users?area=User");//si todo sale bien se redirecciona aqui
                //1: controlador users 2: meotodo de accion users 3:area user
            }
            else
            {
                return Redirect("/Users/Registro");
            }
        }

        private async Task<bool> SaveAsync()//metodo de registro usuarios
        {
            _dataInput = Input;
            var valor = false;

            if (ModelState.IsValid)//si la propiedad devuelve falso es por que tenemos un mensaje de validacion  
            {
                var userList = _userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();
                //se llama al de arriba, poner nombre de la tabla sql, y se hace una consulta de acuerdo a la siguiente condicion, y se consulta email y se verifica que no se repita el dato, to list para hacerlo lista
                if (userList.Count.Equals(0))//si devuelte contador 0 entoces es por que no esta registrado
                {
                    //si todo esta correcto 
                    var strategy = _context.Database.CreateExecutionStrategy();//se indica a base de datos la crecion de estrategia para hacer transacciones ya q se insertan datos a doa tablas simultaneamente 
                    await strategy.ExecuteAsync(async () =>
                    {
                        using (var transaction = _context.Database.BeginTransaction())//se indica a base de datos que aremos transacciones y se almacenan en memoria he insertara en sus debidas tablas
                        {
                            try
                            {
                                var user = new IdentityUser //para registrar un usuario en base de datos 
                                {
                                    UserName = Input.Email,
                                    Email = Input.Email,
                                    PhoneNumber = Input.PhoneNumber
                                };
                                var result = await _userManager.CreateAsync(user, Input.Password);//Toma de user y le pasamos contraseña pero esta debe tener unos parametros minimos
                                if (result.Succeeded)
                                {
                                    await _userManager.AddToRoleAsync(user, Input.Role);//sele adiciona el rol al usuario 
                                    var dataUser = _userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList().Last();//obtener el usuario registrado

                                    //sigue le manejo de imagen
                                    var imageByte = await _uploadimage.ByteAvatarImageAsync(
                                        Input.AvatarImage, _environment, "images/images/default.png");//segundo parametro es la interface de la applicacion
                                    var t_user = new TUsers
                                    {
                                        Name = Input.Name,
                                        LastName = Input.LastName,
                                        NID = Input.NID,
                                        Email = Input.Email,
                                        IdUser = dataUser.Id,//como el token
                                        Image = imageByte,
                                    };
                                    await _context.AddAsync(t_user);
                                    _context.SaveChanges();//se guardan los cambios
                                    transaction.Commit();//conmin se le dice que fue exitoso y opuede insertar la informacion en las dos tablas 
                                    _dataInput = null;
                                    valor = true;
                                }
                                else
                                {
                                    foreach (var item in result.Errors)//no da la coleccion de errores y se almacena en item
                                    {
                                        _dataInput.ErrorMessage = item.Description;//para poder mostrar el mesanje 

                                    }
                                    valor = false;
                                    transaction.Rollback();//si da error se borra toda la informacion de la transaccion  y se libera la tabla igual que abajo 
                                }
                            }
                            catch (Exception ex)
                            {
                                _dataInput.ErrorMessage = ex.Message;
                                transaction.Rollback();//se llama al metodo y se elimina si algo sale mal de la transaccion;
                                valor = false;

                            }
                        }
                    });
                }
                else
                {
                    _dataInput.ErrorMessage = $"el {Input.Email} ya esta registrado";
                    valor = false;
                }
            }
            else
            {
                foreach (var modelState in ModelState.Values)//se captura los datos del model state 
                {
                    foreach (var error in modelState.Errors)//propiedad de coleccion de errores 
                    {
                        _dataInput.ErrorMessage += error.ErrorMessage;
                    }
                }
                valor = false;
            }
            return valor;
        }
    }
}

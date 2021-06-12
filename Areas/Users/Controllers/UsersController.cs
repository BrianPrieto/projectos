using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Areas.Users.Models;
using SalesSystem.Data;
using SalesSystem.Library;
using SalesSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Areas.Users.Controllers
{
    [Area("Users")]
    public class UsersController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        private LUser _user;
        private static DataPaginador<InputModelRegister> models;

        public UsersController(//constructor
            UserManager<IdentityUser>userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _signInManager = signInManager;
            //instancia
            _user = new LUser(userManager, signInManager, roleManager, context);
        }
        public IActionResult Users()
        {
            return View();
        }
    }
} 

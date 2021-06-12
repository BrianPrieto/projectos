using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using SalesSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Library
{
    public class ListObject
    {
        //atributos 
        public LUserRoles _userRoles;

        public IdentityError _identityError;
        public ApplicationDbContext _context;
        public IWebHostEnvironment _environment; //el host de la aplicacion 

        public RoleManager<IdentityRole> _roleManager;
        public UserManager<IdentityUser> _userManager;
        public SignInManager<IdentityUser> _signInManager;

    }
}

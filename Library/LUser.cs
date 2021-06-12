using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesSystem.Areas.Users.Models;
using SalesSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Library
{
    public class LUser : ListObject
    {
        //clase para poder capturar la informacion
        public LUser(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            //se incializan 
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
            _userRoles = new LUserRoles(); 
        }
        public async Task<List<InputModelRegister>> getTUsuariosAsync(String valor, int id)
        {
            List<TUsers> listUser;
            List<SelectListItem> _listRoles;
            List<InputModelRegister> userlist = new List<InputModelRegister>();

            //filtrar usuarios
            if (valor == null && id.Equals(0))//es por que no tengo :'v
            {
                listUser = _context.TUsers.ToList();//TUser hace referencia a la tabla de la base de datos
            }
            else
            {
                if (id.Equals(0))
                {
                    //startswith es una consulta para que coincida con el partametro valor 
                    listUser = _context.TUsers.Where(u => u.NID.StartsWith(valor) || u.Name.StartsWith(valor) ||
                    u.LastName.StartsWith(valor) || u.Email.StartsWith(valor)).ToList();//verificara uno por uno 
                }
                else
                {
                    //en busca de esa id y comparando si es igual al dato por el parametro 
                    listUser = _context.TUsers.Where(u => u.ID.Equals(id)).ToList();//si esto se cumple se obtiene la informacion 
                }
            }
            //verificar si el objeto listUser tiene datos 
            if (!listUser.Count.Equals(0))// es por que tiene datos
            {
                foreach (var item in listUser)
                {
                    //se espera a user role el cual se le pasa usuario rol y id
                    _listRoles = await _userRoles.getRole(_userManager, _roleManager, item.IdUser);

                    //consulta a tabla user por defecto 
                    var user = _context.Users.Where(u => u.Id.Equals(item.IdUser)).ToList().Last();
                    //ya tenemos la informacion de ambas tablas se hace una colecion de datos utilizando la informacion
                    userlist.Add(new InputModelRegister
                    {
                        Id = item.ID,
                        ID = item.IdUser,
                        NID = item.NID,
                        Name = item.Name,
                        LastName = item.LastName,
                        Email = item.Email,
                        Role = _listRoles[0].Text,
                        Image = item.Image,
                        IdentityUser = user
                    });
                }
            }
        }
    }
}

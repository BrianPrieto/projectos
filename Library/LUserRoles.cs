using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Library
{
    //ahora se crea un procedimiento para obtener los roles y poder visualizarlos en la interfaz registroUser
    public class LUserRoles
    {
        public List<SelectListItem> getRoles(RoleManager<IdentityRole> roleManager)//role manager nos permite administrar los roles creados
        {
            List<SelectListItem> _selectList = new List<SelectListItem>();//_seleclist contendra coleccion de objetos de seleclistitem
            var roles = roleManager.Roles.ToList();//sele asigna un parametro ppara llamar a la propiedad role que tiene la coleccion, despues se pasa a coleccion de datos
            roles.ForEach(item =>//se obtiene cada objeto
            {
                _selectList.Add(new SelectListItem //se le agrega la coleccion de datos que se le da lo que obtendremos
                {
                    Value = item.Id,
                    Text = item.Name
                });
            });
            return _selectList;//este contiene la coleccion de roles  
        }
        public async Task<List<SelectListItem>> getRole(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, string ID)
        {
            List<SelectListItem> _selectList = new List<SelectListItem>();
            var users = await userManager.FindByIdAsync(ID); //objeto que contiene la informacion de usyuario empezando por su id
            var roles = await userManager.GetRolesAsync(users);
            if (roles.Count.Equals(0))//cuantos elementos tenemos almacenados  si es 0 es por que no tenemos registroa 
            {
                _selectList.Add(new SelectListItem
                {
                    Value = "0",
                    Text = "No role"
                });
            }
            else
            {
                //vamos a obtenerlos 
                //con name llamo al nombre del rol y lo obtendremos de la posicion 0 si los nombres son iguales 
                var roleUser = roleManager.Roles.Where(m => m.Name.Equals(roles[0]));//se agrega a roleuser que sera una cooleccion de la clase identiity roole
                foreach (var Data in roleUser)
                {
                    _selectList.Add(new SelectListItem
                    {
                            Value =Data.Id,
                            Text = Data.Name
                    });//se obtiene la informacion completa del rol de usuario 
                }
            }
            return _selectList;

        }
    } 
}

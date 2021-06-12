using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Areas.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesSystem.Data
{//clase que contiene toda la informacion a la base de datos 
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TUsers> TUsers { get; set; }// se crea la propiedad <> y con eso se gestiona toda la informacion en TUsers asiganar,obtener,eliminar etc
        //y debemos crear la tabla eb herramientas consola de administrador de paquetes para hacer una nueva migracion

    }
}

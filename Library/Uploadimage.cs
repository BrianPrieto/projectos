using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Library
{
    public class Uploadimage //aqui se trata la imagen que se carga 
    {
        public async Task<byte[]> ByteAvatarImageAsync(IFormFile AvatarImage, IWebHostEnvironment environment, string image)//se convetira en tipo byte para hacer un insert en una tabla 
        {
           
            if (AvatarImage != null)//si es distinto a null es por que contiene la imagen cargada 
            {
                using (var memoryStream = new MemoryStream())
                {
                    await AvatarImage.CopyToAsync(memoryStream); //obtiene toda la inFORMACION DE OBJETO avatarimage
                    return memoryStream.ToArray();// convierto toda la informacion de la dfoto de un array si la condicion se cumple
                }
            }
            else
            {
                var archivoOrigen = $"{environment.ContentRootPath}/wwwroot/{image}";
                return File.ReadAllBytes(archivoOrigen);//una vez que tenga la imagen la convertira en un tipo array para retornarla entonces obtendremos la image por defecto 
            }
        }
    }
}

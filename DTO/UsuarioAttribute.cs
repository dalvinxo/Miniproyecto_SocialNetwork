using Database.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class UsuarioAttribute : ValidationAttribute
    {


        public override bool IsValid(object value)
        {

            SocialNetworkContext context = new SocialNetworkContext();

            var ListUsuario = context.TablaUsuario.Select(x => x.NombreUsuario).ToList();

            if (ListUsuario.Contains(value))
            {
                return false;
            }

            return true;
        }

    }



}

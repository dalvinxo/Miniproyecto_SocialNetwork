using Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DTO
{
    //Not time!!!!!
    class AddFriendsDTO
    {
        [FriendsValidation(ErrorMessage = "El nombre del amigo no existe, intenta con otro!")]
        [Required(ErrorMessage = "Este campo debe ser llenado")]
        [Display(Name = "Friends: ")]
        [StringLength(30)]
        public string Friends { get; set; }

        [UsuarioValidacion(ErrorMessage = "Usuario no existe!")]
        [Required(ErrorMessage = "Este campo debe ser llenado")]
        [Display(Name = "Usuario: ")]
        [StringLength(30)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Este campo debe ser llenado")]
        [Display(Name = "Clave: ")]
        [StringLength(30)]
        public string Clave { get; set; }
    }

    public class FriendsValidationAttribute : ValidationAttribute
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

      public class UsuarioValidacionAttribute : ValidationAttribute
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

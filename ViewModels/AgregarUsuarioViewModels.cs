using Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class AgregarUsuarioViewModels
    {

        [Friends(ErrorMessage = "Este Usuario No existe dentro de sistema!")]
        [Required(ErrorMessage = "Este campo debe ser llenado")]
        [Display(Name = "Usuario: ")]
        [StringLength(30)]
        public string NombreUsuario { get; set; }
        public int UserIdUsuario { get; set; }

    }


    public class FriendsAttribute : ValidationAttribute
    {


        public override bool IsValid(object value)
        {

            SocialNetworkContext context = new SocialNetworkContext();

            var ListUsuario = context.TablaUsuario.Select(x => x.NombreUsuario).ToList();

            if (ListUsuario.Contains(value))
            {
                return true;
            }

            return false;
        }

    }



}

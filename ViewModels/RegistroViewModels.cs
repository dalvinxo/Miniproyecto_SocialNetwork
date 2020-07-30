using Database.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class RegistroViewModels
    {
       
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Este campo debe ser llenado")]
        [Display(Name = "Nombre:")]
        [StringLength(40)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo debe ser llenado")]
        [Display(Name = "Apellido:")]
        [StringLength(45)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Este campo debe ser llenado")]
        [Display(Name = "Correo:")]
        [StringLength(35)]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Este campo debe ser llenado")]
        [Display(Name = "Telefono:")]
        [StringLength(40)]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [Usuario(ErrorMessage = "Este usuario ya existe!")]
        [Required(ErrorMessage = "Este campo debe ser llenado")]
        [Display(Name = "Usuario: ")]
        [StringLength(30)]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Este campo debe ser llenado")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña:")]
        [StringLength(25)]
        public string Clave { get; set; }

        [Display(Name = "Confirmar Contraseña:")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirmar La Contraseña")]
        [Compare(nameof(Clave), ErrorMessage = "La confirmación no se parece a la contraseña!")]
        public string ConfirmClave { get; set; }

        [Display(Name = "Foto de Perfil")]
        public IFormFile FotoProfile { get; set; }

        public string FotoPerfil { get; set; }

        public string Estado { get; set; }

    }

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

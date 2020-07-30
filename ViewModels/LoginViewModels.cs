using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class LoginViewModels
    {

        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Debes colocar un usuario!")]
        [Display(Name = "Usuario: ")]
        [StringLength(30)]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Debes colocar una contraseña!")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña: ")]
        [StringLength(25)]
        public string Clave { get; set; }

    }
}

using Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DTO
{
    public class PublicarUsuarioDTO
    {

        public int IdPublicacion { get; set; }

        [Required(ErrorMessage = "Este campo debe ser llenado")]
        [Display(Name = "Usuario: ")]
        [StringLength(30)]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Este campo debe ser llenado")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña:")]
        [StringLength(25)]
        public string Clave { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio.")]
        [Display(Name = "Titulo de la Publicacion:")]
        [StringLength(80)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Este TextArea no puede estar vacio")]
        [Display(Name = "Cuerpo de la publicación:")]
        [StringLength(1500)]
        public string Cuerpo { get; set; }

    }



}

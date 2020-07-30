using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class PublicacionEditViewModels
    {

        public int IdPublicacion { get; set; }

        [Required(ErrorMessage = "Debes colocarle un titulo a tu publicacion.")]
        [Display(Name = "Titulo de la Publicacion:")]
        [StringLength(80)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Este TextArea no puede estar vacio.")]
        [Display(Name = "Cuerpo de la publicación:")]
        [StringLength(1500)]
        public string Cuerpo { get; set; }

        [Display(Name = "Actualizar Foto: ")]
        public IFormFile FotoIFormFilePublicacion { get; set; }

        public DateTime Fecha { get; set; }
        public string FotoPublicacion { get; set; }


        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FotoPerfil { get; set; }


    }


}

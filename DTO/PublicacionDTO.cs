using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class PublicacionDTO
    {

        public int IdPublicacion { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio.")]
        [StringLength(80)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Este TextArea no puede estar vacio")]
        [StringLength(1500)]
        public string Cuerpo { get; set; }

        public DateTime Fecha { get; set; }

        public string FotoPublicacion { get; set; }

        [Required(ErrorMessage = "Este TextArea no puede estar vacio")]
        public int IdUsuario { get; set; }



    }

}

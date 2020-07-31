using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class ComentariosDTO
    {

        public int IdComentario { get; set; }

        [Required(ErrorMessage = "Debes colocar el Id del Usuario aquien pertenece este comentario")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "el IdPublicacion es requirido!")]
        public int IdPublicacion { get; set; }

        [Required(ErrorMessage = "Este campo debe ser llenado")]
        public string UserComentario { get; set; }

        public DateTime Fecha { get; set; }

    }

}

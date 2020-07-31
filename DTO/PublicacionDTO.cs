using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class PublicacionDTO
    {

        public int IdPublicacion { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public DateTime Fecha { get; set; }
        public string FotoPublicacion { get; set; }
        public int IdUsuario { get; set; }

    }

}

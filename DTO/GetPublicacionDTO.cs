using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class GetPublicacionDTO
    {

        public int IdPublicacion { get; set; }

        public string Titulo { get; set; }

        public string Cuerpo { get; set; }

        public DateTime Fecha { get; set; }

        public string FotoPublicacion { get; set; }

        public int IdUsuario { get; set; }

        public List<GetComentariosDTO> ListComentariosDTO { set; get; }

    }
}

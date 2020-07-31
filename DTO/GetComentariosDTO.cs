using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class GetComentariosDTO
    {

        public int IdComentario { get; set; }

        public int IdUsuario { get; set; }

        public int IdPublicacion { get; set; }

        public string UserComentario { get; set; }

        public DateTime Fecha { get; set; }

        public List<SubComentariosDTO> ListSubComentariosDTO { set; get; }
    }
}

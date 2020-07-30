using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class TablaComentarios
    {
        public TablaComentarios()
        {
            SubTablaComentarios = new HashSet<SubTablaComentarios>();
        }

        public int IdComentario { get; set; }
        public int IdUsuario { get; set; }
        public int IdPublicacion { get; set; }
        public string UserComentario { get; set; }
        public DateTime Fecha { get; set; }

        public virtual TablaPublicaciones IdPublicacionNavigation { get; set; }
        public virtual TablaUsuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<SubTablaComentarios> SubTablaComentarios { get; set; }
    }
}

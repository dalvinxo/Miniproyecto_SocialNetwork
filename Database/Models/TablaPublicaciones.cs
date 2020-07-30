using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class TablaPublicaciones
    {
        public TablaPublicaciones()
        {
            TablaComentarios = new HashSet<TablaComentarios>();
        }

        public int IdPublicacion { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public DateTime Fecha { get; set; }
        public string FotoPublicacion { get; set; }
        public int IdUsuario { get; set; }

        public virtual TablaUsuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<TablaComentarios> TablaComentarios { get; set; }
    }
}

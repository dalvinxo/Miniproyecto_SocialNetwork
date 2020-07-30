using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class SubTablaComentarios
    {
        public int IdSubComentario { get; set; }
        public string UserComentario { get; set; }
        public int IdUsuario { get; set; }
        public int IdComentario { get; set; }
        public DateTime Fecha { get; set; }

        public virtual TablaComentarios IdComentarioNavigation { get; set; }
        public virtual TablaUsuario IdUsuarioNavigation { get; set; }
    }
}

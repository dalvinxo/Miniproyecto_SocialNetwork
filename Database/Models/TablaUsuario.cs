using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class TablaUsuario
    {
        public TablaUsuario()
        {
            SubTablaComentarios = new HashSet<SubTablaComentarios>();
            TablaAmigoFriendsIdUsuarioNavigation = new HashSet<TablaAmigo>();
            TablaAmigoUserIdUsuarioNavigation = new HashSet<TablaAmigo>();
            TablaComentarios = new HashSet<TablaComentarios>();
            TablaPublicaciones = new HashSet<TablaPublicaciones>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string FotoPerfil { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<SubTablaComentarios> SubTablaComentarios { get; set; }
        public virtual ICollection<TablaAmigo> TablaAmigoFriendsIdUsuarioNavigation { get; set; }
        public virtual ICollection<TablaAmigo> TablaAmigoUserIdUsuarioNavigation { get; set; }
        public virtual ICollection<TablaComentarios> TablaComentarios { get; set; }
        public virtual ICollection<TablaPublicaciones> TablaPublicaciones { get; set; }
    }
}

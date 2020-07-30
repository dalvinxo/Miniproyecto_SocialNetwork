using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class TablaAmigo
    {
        public int UserIdUsuario { get; set; }
        public int FriendsIdUsuario { get; set; }

        public virtual TablaUsuario FriendsIdUsuarioNavigation { get; set; }
        public virtual TablaUsuario UserIdUsuarioNavigation { get; set; }
    }
}

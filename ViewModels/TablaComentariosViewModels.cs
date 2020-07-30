using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class TablaComentariosViewModels
    {
        public int IdComentario { get; set; }
        public int IdUsuario { get; set; }
        public int IdPublicacion { get; set; }

        [StringLength(200)]
        public string UserComentario { get; set; }
        public DateTime Fecha { get; set; }

    }
}

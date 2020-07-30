using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class ComentarioPlantilla
    {

        public int IdComentario { get; set; }
        public int IdUsuario { get; set; }
        public int IdPublicacion { get; set; }
        public string UserComentario { get; set; }
        public DateTime Fecha { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FotoPerfil { get; set; }
    }
}

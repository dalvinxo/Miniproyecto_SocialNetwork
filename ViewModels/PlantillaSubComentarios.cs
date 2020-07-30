using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class PlantillaSubComentarios
    {

        public int IdSubComentario { get; set; }
        public string UserComentario { get; set; }
        public int IdUsuario { get; set; }
        public int IdComentario { get; set; }
        public DateTime Fecha { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FotoPerfil { get; set; }

    }

}

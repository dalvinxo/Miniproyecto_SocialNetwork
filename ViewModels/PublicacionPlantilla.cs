using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class PublicacionPlantilla
    {

        public int IdPublicacion { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public DateTime Fecha { get; set; }
        public string FotoPublicacion { get; set; }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FotoPerfil { get; set; }


    }

}

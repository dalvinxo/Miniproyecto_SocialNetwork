using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class GetUsuarioAmigosDTO
    {

        public int IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public string NombreUsuario { get; set; }

        public List<AmigosDTO> ListaAmigosUsuario { get; set; }

    }

}

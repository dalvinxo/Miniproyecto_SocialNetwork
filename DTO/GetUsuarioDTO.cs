using Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DTO
{
    public class GetUsuarioDTO
    {

    

            public int IdUsuario { get; set; }
       
            public string Nombre { get; set; }

            public string Apellido { get; set; }

            public string Correo { get; set; }

            public string Telefono { get; set; }

            public string NombreUsuario { get; set; }

            public string Clave { get; set; }

            public string FotoPerfil { get; set; }

            public string Estado { get; set; }

            public List<GetPublicacionDTO> ListPublicacionDTO { get; set; }
      
        }


}








using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
     public class PublicacionesFriendsViewModels
    {   

        public virtual UsuarioLogueadoPlantilla UsuarioLogueado { get; set; }
        public virtual TablaComentariosViewModels ComentariosUsuario { get; set; }
        public virtual AmigoViewModels AmigoUsuario { get; set; }
        public List<PlantillaAmigos> ListaAmigosPlantilla { get; set; }
        public List<PlantillaSubComentarios> ListaSubComentarioPlantilla { get; set; }
        public List<PublicacionPlantilla> ListaPublicacionPlantilla { get; set; }
        public List<ComentarioPlantilla> ListaComentarioPlantilla { get; set; }



    }



}

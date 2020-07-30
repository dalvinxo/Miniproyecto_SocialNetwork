using Database.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class PublicacionUsuarioViewModels
    {


        public int IdPublicacion { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio.")]
        [Display(Name = "Titulo de la Publicacion:")]
        [StringLength(80)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Este TextArea no puede estar vacio")]
        [Display(Name = "Cuerpo de la publicación:")]
        [StringLength(1500)]
        public string Cuerpo { get; set; }

        public DateTime Fecha { get; set; }
        
        public int IdUsuario { get; set; }

        [Display(Name = "Foto de la publicacion: ")]
        public IFormFile FotoIformfilePublicacion { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string FotoPerfil { get; set; }

        public string FotoPublicacion { get; set; }


        public virtual TablaComentariosViewModels ComentariosUsuario { get; set; }


        public List<PlantillaSubComentarios> ListaSubComentarioPlantilla { get; set; }
        public List<PublicacionPlantilla> ListaPublicacionPlantilla { get; set; }
        public List<ComentarioPlantilla> ListaComentarioPlantilla { get; set; }


    }





}

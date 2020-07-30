using AutoMapper;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace Miniproyecto_SocialNetwork.Infrastructure
{
    public class Mapping : Profile
    {

        public Mapping()
        {
            MapearRegistroUsuario();
            MapearPublicacionPos();
            MapearViewPublicacionPlantilla();
            MapearComentario();
            MapearViewComentarioPlantilla();
            MapearSubComentario();
            MapearViewSubComentarioPlantilla();

        }

        private void MapearRegistroUsuario()
        {
            CreateMap<RegistroViewModels, TablaUsuario>().ReverseMap().
                        ForMember(dest => dest.ConfirmClave, opt => opt.Ignore()).
                        ForMember(dest => dest.FotoProfile, opt => opt.Ignore());
        }

        private void MapearPublicacionPos()
        {
            CreateMap<PublicacionUsuarioViewModels, TablaPublicaciones>().ReverseMap().
                        ForMember(dest => dest.FotoIformfilePublicacion, opt => opt.Ignore()).
                        ForMember(dest => dest.Nombre, opt => opt.Ignore()).
                        ForMember(dest => dest.Apellido, opt => opt.Ignore()).
                       ForMember(dest => dest.FotoPerfil, opt => opt.Ignore()).
                        ForMember(dest => dest.ComentariosUsuario, opt => opt.Ignore()).
                        ForMember(dest => dest.ListaComentarioPlantilla, opt => opt.Ignore()).
                          ForMember(dest => dest.ListaSubComentarioPlantilla, opt => opt.Ignore()).
                        ForMember(dest => dest.ListaPublicacionPlantilla, opt => opt.Ignore())
                        ;
        }

        private void MapearViewPublicacionPlantilla()
        {
            CreateMap<PublicacionPlantilla, TablaPublicaciones>().ReverseMap().
                        ForMember(dest => dest.Nombre, opt => opt.Ignore()).
                        ForMember(dest => dest.Apellido, opt => opt.Ignore()).
                       ForMember(dest => dest.FotoPerfil, opt => opt.Ignore())
                        ;
        }

        private void MapearViewComentarioPlantilla()
        {
            CreateMap<ComentarioPlantilla, TablaComentarios>().ReverseMap().
                        ForMember(dest => dest.Nombre, opt => opt.Ignore()).
                        ForMember(dest => dest.Apellido, opt => opt.Ignore()).
                       ForMember(dest => dest.FotoPerfil, opt => opt.Ignore())
                        ;
        }

        private void MapearViewSubComentarioPlantilla()
        {
            CreateMap<PlantillaSubComentarios, SubTablaComentarios>().ReverseMap().
                        ForMember(dest => dest.Nombre, opt => opt.Ignore()).
                        ForMember(dest => dest.Apellido, opt => opt.Ignore()).
                       ForMember(dest => dest.FotoPerfil, opt => opt.Ignore())
                        ;
        }


        private void MapearComentario()
        {
            CreateMap<TablaComentariosViewModels, TablaComentarios>().ReverseMap();
        }

        private void MapearSubComentario()
        {
            CreateMap<SubComentariosViewModels, SubTablaComentarios>().ReverseMap();
        }


    }
}

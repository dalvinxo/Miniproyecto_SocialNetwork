using AutoMapper;
using Database.Models;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.AutoMapper
{
    public class ConfigurationProfile : Profile
    {

        public ConfigurationProfile(){
            MapearUsuarioDTOUsuarioModel();
            MapearPublicacionDTOPublicacionModel();
            MapearComentarioDTOComentariosModel();
            MapearSubComentrioDTO();
            MapearGetUsuarioAmigoDTO();
            MapearAmigoDTO();
            MapearPublicarUsuarioDTO();
        }

        private void MapearUsuarioDTOUsuarioModel()
        {
            CreateMap<GetUsuarioDTO, TablaUsuario>().ReverseMap().
                        ForMember(dest => dest.ListPublicacionDTO, opt => opt.Ignore());
        }

        private void MapearComentarioDTOComentariosModel()
        {
            CreateMap<GetComentariosDTO, TablaComentarios>().ReverseMap().
                ForMember(dest => dest.ListSubComentariosDTO, opt => opt.Ignore()); ;
        }

        private void MapearPublicacionDTOPublicacionModel()
        {
            CreateMap<GetPublicacionDTO, TablaPublicaciones>().ReverseMap().
                ForMember(dest => dest.ListComentariosDTO, opt => opt.Ignore()); 
        }

        private void MapearSubComentrioDTO()
        {
            CreateMap<SubComentariosDTO, SubTablaComentarios>().ReverseMap();

        }


        private void MapearGetUsuarioAmigoDTO()
        {
            CreateMap<GetUsuarioAmigosDTO, TablaUsuario>().ReverseMap().
                           ForMember(dest => dest.ListaAmigosUsuario, opt => opt.Ignore()); 

        }

        private void MapearAmigoDTO()
        {
            CreateMap<AmigosDTO, TablaUsuario>().ReverseMap();

        }

        private void MapearPublicarUsuarioDTO()
        {
            CreateMap<PublicarUsuarioDTO, TablaPublicaciones>().ReverseMap().
                            ForMember(dest => dest.NombreUsuario, opt => opt.Ignore()).
                            ForMember(dest => dest.Clave, opt => opt.Ignore());

        }


    }
}

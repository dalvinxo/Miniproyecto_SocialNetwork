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
        }

        private void MapearRegistroUsuario()
        {
            CreateMap<RegistroViewModels, TablaUsuario>().ReverseMap().
                        ForMember(dest => dest.ConfirmClave, opt => opt.Ignore()).
                        ForMember(dest => dest.FotoProfile, opt => opt.Ignore());
        }


    }
}

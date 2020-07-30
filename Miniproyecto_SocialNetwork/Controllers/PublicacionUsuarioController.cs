using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace Miniproyecto_SocialNetwork.Controllers
{

   [Authorize]
    public class PublicacionUsuarioController : Controller
    {

        private readonly TablaUsuarioRepository _tablaUsuarioRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<IdentityUser> _signInManager;

        public PublicacionUsuarioController(IMapper mapper, TablaUsuarioRepository tablaUsuarioRepository,
            SignInManager<IdentityUser> signInManager)
        {
         
            _mapper = mapper;
            _tablaUsuarioRepository = tablaUsuarioRepository;
            _signInManager = signInManager;

        }


   
        public IActionResult Home()
        {

            string nameUser = User.Identity.Name;
            ViewBag.usuarioNombre = nameUser;
            return View();
        }

    }
}

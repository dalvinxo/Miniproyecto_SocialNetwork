using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Database.Models;
using Repository.Repository;
using AutoMapper;
using ViewModels;
using Email;
using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices.ComTypes;

namespace Miniproyecto_SocialNetwork.Controllers
{
    public class AccountController : Controller
    {

        private readonly TablaUsuarioRepository _tablaUsuarioRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(TablaUsuarioRepository tablaUsuarioRepository, IEmailSender emailSender, 
            SignInManager<IdentityUser> signInManager)
        {

            _signInManager = signInManager;
            _tablaUsuarioRepository = tablaUsuarioRepository;
            _emailSender = emailSender;

        }
        
        
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "PublicacionUsuario");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModels VerifyUser)
        {

            if (ModelState.IsValid) {

               
                var verificacion = await _tablaUsuarioRepository.VerifyUserIdentiy(VerifyUser);

                if (verificacion)
                {
                   var estado = await _tablaUsuarioRepository.VerificarUsuarioEstado(VerifyUser.NombreUsuario);

                    if (estado.Estado == "Inactivo")
                    {
                        await _signInManager.SignOutAsync();
                        return RedirectToAction("VerificarGmail", "Advertencia", new { NameUsuario = VerifyUser.NombreUsuario });

                    }

                    return RedirectToAction("Home", "PublicacionUsuario");
                }

                ModelState.AddModelError("", "Por favor verificar su usario y contraseña...");
                return View(VerifyUser);
          
            }

                      return View(VerifyUser);
        }

        public IActionResult CerrarSesion()
        {

            _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }


        public async Task<ActionResult> ConfirmAccount(int? Id)
        {

            await _tablaUsuarioRepository.ActualizarEstado(Id.Value);
            return RedirectToAction("Login", "Account");
        }






    }
}

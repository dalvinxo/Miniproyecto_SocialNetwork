using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace Miniproyecto_SocialNetwork.Controllers
{
    public class AdvertenciaController : Controller
    {

        private readonly TablaUsuarioRepository _tablaUsuarioRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AdvertenciaController(TablaUsuarioRepository tablaUsuarioRepository, IEmailSender emailSender,
            SignInManager<IdentityUser> signInManager)
        {

            _signInManager = signInManager;
            _tablaUsuarioRepository = tablaUsuarioRepository;
            _emailSender = emailSender;

        }


        public IActionResult VerificarEstadoCuenta(string NameUsuario)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "PublicacionUsuario");
            }

            ViewBag.Name = NameUsuario;

            return View();
        }


        public IActionResult VerificarGmail(string NameUsuario)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "PublicacionUsuario");
            }

            ViewBag.Name = NameUsuario;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RestaurarCuentaAsync(string NameUsuario) {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "PublicacionUsuario");
            }


            var usuario = await _tablaUsuarioRepository.VerificarUsuarioEstado(NameUsuario);

            if (usuario == null) {

                ViewBag.omg = 1;
                ViewBag.Name = NameUsuario;
                return View();
            }
            else{
                string clave = randomClave(7);
                ViewBag.omg = 2;
                ViewBag.Correo = usuario.Correo;

                try
                {
                    await _tablaUsuarioRepository.ActualizarClave(usuario, clave);
                }
                catch {
                    return BadRequest();
                }
                var message = new Message(new string[] {usuario.Correo}, "Seguridad - Social Network 2", "Saludos usuario: " + usuario.NombreUsuario + ", Esta es su nueva contraseña: "+clave+". ");
                await _emailSender.SendMailAsync(message);

                return View();
            }


        }


        public string randomClave(int Countcaracteres)
        {


            //Caracteres opcionales: abcdefghijklmnopqrstuvwxyz
            string Caracteres = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789_";
            Random random = new Random();

            char[] chars = new char[Countcaracteres];

            for (int i = 0; i < Countcaracteres; i++)
            {
                chars[i] = Caracteres[random.Next(0, Caracteres.Length)];
            }

            return new string(chars);


        }



    }
}

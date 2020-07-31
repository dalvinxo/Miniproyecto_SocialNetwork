using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Database.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using ViewModels;
using Email;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Miniproyecto_SocialNetwork.Controllers
{


    public class RegistroController : Controller
    {

        private readonly TablaUsuarioRepository _reposUsuario;
        private readonly IMapper _mapper;
        [Obsolete]
        private readonly IHostingEnvironment hostingEnvironment;

        private readonly IEmailSender _emailSender;


        [Obsolete]
        public RegistroController(IMapper mapper, TablaUsuarioRepository usuarioRepository, 
            IHostingEnvironment hostingEnvironment, IEmailSender emailSender)
        {

            _mapper = mapper;
            _reposUsuario = usuarioRepository;
            this.hostingEnvironment = hostingEnvironment;
            _emailSender = emailSender;

        }



        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "PublicacionUsuario");
            }

                return View();
        }

        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> Index(RegistroViewModels NewUser) {

            if (ModelState.IsValid) {

                string uniqueName = null;

                if (NewUser.FotoProfile == null)
                {
                    ModelState.AddModelError("","Debes subir una foto de perfil.");
                    return View(NewUser);


                }



                if (NewUser.FotoProfile != null) {
                        
                    var FolderPath = Path.Combine(hostingEnvironment.WebRootPath, "images/fotoPerfil");

                    uniqueName = Guid.NewGuid().ToString() + "name" + NewUser.FotoProfile.FileName;

                    var FilePath = Path.Combine(FolderPath, uniqueName);

                    if (FilePath != null) {
                        var stream = new FileStream(FilePath, FileMode.Create);
                        NewUser.FotoProfile.CopyTo(stream);
                        stream.Flush();
                        stream.Close();
                    }


                }

                NewUser.FotoPerfil = uniqueName;
                var Usuario = _mapper.Map<TablaUsuario>(NewUser);
                await _reposUsuario.AddAsync(Usuario);
                await _reposUsuario.AddUserIdentity(Usuario);

                //var message = new Message(new string[] { suario.Correo }, "Seguridad - Social Network 2", "Saludos usuario: " + usuario.NombreUsuario + ", Esta es su nueva contraseña: " + clave + ". ");


                var message = new Message(new string[]{Usuario.Correo},"Seguridad Social Network 2","Saludos usuario: "+Usuario.NombreUsuario+ ", por favor entrar a este link: http://localhost:50311/Account/ConfirmAccount/"+Usuario.IdUsuario+". para activar su cuenta.");
                await _emailSender.SendMailAsync(message);


                return RedirectToAction("VerificarEstadoCuenta","Advertencia", new { NameUsuario = Usuario.NombreUsuario});

             }



            return View(NewUser);

        }



    }
}

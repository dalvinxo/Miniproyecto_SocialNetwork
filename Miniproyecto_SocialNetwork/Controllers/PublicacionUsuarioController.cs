using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database.Models;
using Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using Org.BouncyCastle.Asn1.Cms;
using Repository.Repository;
using ViewModels;

namespace Miniproyecto_SocialNetwork.Controllers
{

   [Authorize]
    public class PublicacionUsuarioController : Controller
    {

        private readonly TablaUsuarioRepository _tablaUsuarioRepository;
        private readonly TablaPublicacionRepository _tablaPublicacionRepository;
        private readonly TablaComentarioRepository _tablaComentarioRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<IdentityUser> _signInManager;


        public PublicacionUsuarioController(IMapper mapper, TablaUsuarioRepository tablaUsuarioRepository,
            SignInManager<IdentityUser> signInManager, TablaPublicacionRepository tablaPublicacionRepository,
            TablaComentarioRepository tablaComentarioRepository)
        {
            
            _mapper = mapper;
            _tablaUsuarioRepository = tablaUsuarioRepository;
            _tablaPublicacionRepository = tablaPublicacionRepository;
            _tablaComentarioRepository = tablaComentarioRepository;
            _signInManager = signInManager;

        }

        public async Task<int> IdUsuarioClienteAsync()
        {
            return await _tablaUsuarioRepository.ReturnIdUsuarioLogueado(User.Identity.Name); ;
        }




        public async Task<IActionResult> Home()
        {
         ///   string nameUser = User.Identity.Name;

            
            var PublicacionesUsuario = await _tablaPublicacionRepository.TraarPublicacionesMyUsuario(await IdUsuarioClienteAsync());
            var ComentarioUsuarios = await _tablaPublicacionRepository.TraerComentariosMyUsuario();
            var UsuarioActivo = await _tablaUsuarioRepository.GetByIdAsync(await IdUsuarioClienteAsync());
            

            PublicacionUsuarioViewModels pb = new PublicacionUsuarioViewModels();
            pb.ListaComentarioPlantilla = ComentarioUsuarios;
            pb.ListaPublicacionPlantilla = PublicacionesUsuario;
            ViewBag.usuarioNombre = UsuarioActivo.NombreUsuario;
            ViewBag.PhotoProfile = UsuarioActivo.FotoPerfil;
            ViewBag.Id = await IdUsuarioClienteAsync();

            return View(pb);
        }




        [HttpPost]
        public async Task<IActionResult> Home( PublicacionUsuarioViewModels pbs)
        {


            if (ModelState.IsValid)
            {

                if (pbs.FotoIformfilePublicacion != null)
                {
                    try
                    {
                        pbs.IdUsuario = await IdUsuarioClienteAsync();
                        await _tablaPublicacionRepository.AgregarPublicacion(pbs);
                        return RedirectToAction("Home");
                    }

                    catch {
                        ModelState.AddModelError("", "A ocurrido un problema interno en la base de datos.");
                        return View("Home");

                    }


                }

                ModelState.AddModelError("", "Debes Seleccionar una imagen");
                return View("Home");

            }
       

           return View(pbs);


        }


        [HttpPost]
        public async Task<IActionResult> Comentarios(string comentar, int idpublicacion) 
        {

            if (comentar != null)
            {
                TablaComentariosViewModels coment = new TablaComentariosViewModels();
                ViewBag.Comenta = "";
                coment.UserComentario = comentar;
                coment.IdPublicacion = idpublicacion;
                coment.IdUsuario = await IdUsuarioClienteAsync();
                var comentariO = _mapper.Map<TablaComentarios>(coment);
                await _tablaComentarioRepository.AddAsync(comentariO);
                return RedirectToAction("Home","PublicacionUsuario");
            }
            else {
                ViewBag.Comenta = "No puedes enviar comentarios vacios...";
                return RedirectToAction("Home", "PublicacionUsuario");
            }

        }


        }


 }



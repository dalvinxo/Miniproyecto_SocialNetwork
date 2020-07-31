using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using ViewModels;

namespace Miniproyecto_SocialNetwork.Controllers
{

    [Authorize]
    public class PublicacionFriendsController : Controller
    {

        private readonly TablaUsuarioRepository _tablaUsuarioRepository;
        private readonly TablaPublicacionRepository _tablaPublicacionRepository;
        private readonly TablaComentarioRepository _tablaComentarioRepository;
        private readonly SubTablaComentarioRepository _subTablaComentarioRepository;
        private readonly TablaAmigoRepository _tablaAmigoRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<IdentityUser> _signInManager;


        public PublicacionFriendsController(IMapper mapper, TablaUsuarioRepository tablaUsuarioRepository,
            SignInManager<IdentityUser> signInManager, TablaPublicacionRepository tablaPublicacionRepository,
            TablaComentarioRepository tablaComentarioRepository, SubTablaComentarioRepository subTablaComentarioRepository,
            TablaAmigoRepository tablaAmigoRepository)
        {

            _mapper = mapper;
            _tablaUsuarioRepository = tablaUsuarioRepository;
            _tablaPublicacionRepository = tablaPublicacionRepository;
            _tablaComentarioRepository = tablaComentarioRepository;
            _signInManager = signInManager;
            _subTablaComentarioRepository = subTablaComentarioRepository;
            _tablaAmigoRepository = tablaAmigoRepository;

        }

        public async Task<int> IdUsuarioClienteAsync()
        {
            return await _tablaUsuarioRepository.ReturnIdUsuarioLogueado(User.Identity.Name); ;
        }



        public async Task<IActionResult> HomeFriends()
        {

            await _tablaUsuarioRepository.UsuarioLogueadoPlantilla(await IdUsuarioClienteAsync());
            var PublicacionAmigos = await _tablaUsuarioRepository.TraerPublicacionAmigos(await IdUsuarioClienteAsync());
            var ComentariosAmigos = await _tablaPublicacionRepository.TraerComentariosMyUsuario();
            var SubComentariosAmigos = await _tablaPublicacionRepository.TraerSubComentariosMyUsuario();

            PublicacionesFriendsViewModels pv = new PublicacionesFriendsViewModels();
            pv.UsuarioLogueado = await _tablaUsuarioRepository.UsuarioLogueadoPlantilla(await IdUsuarioClienteAsync());
            pv.ListaAmigosPlantilla = await _tablaUsuarioRepository.AmigosDelUsuario(await IdUsuarioClienteAsync());
            pv.ListaPublicacionPlantilla = PublicacionAmigos;
            pv.ListaComentarioPlantilla = ComentariosAmigos;
            pv.ListaSubComentarioPlantilla = SubComentariosAmigos;

            return View(pv);
        }

        //public IActionResult HomeFriends()
        //{
        //    return View();
        //}


        public async Task<IActionResult> AddFriends(int? id)
        {

            AgregarUsuarioViewModels po = new AgregarUsuarioViewModels();
            po.UserIdUsuario = id.Value;

            return View(po);
        }

            [HttpPost]
            public async Task<IActionResult> AddFriends(AgregarUsuarioViewModels ok)
            {

                if (ModelState.IsValid) {

                var amigo = await _tablaAmigoRepository.AddAmigos(ok);

                if (amigo) {

                    return RedirectToAction("HomeFriends", "PublicacionFriends");

                }
                return View(ok);

            }

                return View(ok);
            }

        [HttpPost]
        public async Task<IActionResult> ComentariosFriends(string comentar, int idpublicacion)
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
                return RedirectToAction("HomeFriends", "PublicacionFriends");
            }
            else
            {
                ViewBag.Comenta = "No puedes enviar comentarios vacios...";
                return RedirectToAction("HomeFriends", "PublicacionFriends");

            }

        }

        [HttpPost]
        public async Task<IActionResult> SubComentarFriends(string comentar, int IdComentario)
        {

            if (comentar != null)
            {
                SubComentariosViewModels coment = new SubComentariosViewModels();
                ViewBag.Comentare = "";
                coment.UserComentario = comentar;
                coment.IdComentario = IdComentario;
                coment.IdUsuario = await IdUsuarioClienteAsync();
                var subComentar = _mapper.Map<SubTablaComentarios>(coment);
                await _subTablaComentarioRepository.AddAsync(subComentar);
                return RedirectToAction("HomeFriends", "PublicacionFriends");
            }
            else
            {
                ViewBag.Comentare = "No puedes enviar respuestas vacios...";
                return RedirectToAction("HomeFriends", "PublicacionFriends");
            }

        }

        [HttpPost]
        public async Task<IActionResult> EliminarAmigo(int IdUsuario, int IdAmigo) {


            var Full = new TablaAmigo();
            Full.UserIdUsuario = IdUsuario;
            Full.FriendsIdUsuario = IdAmigo;

            await _tablaAmigoRepository.DeleteEntity(Full);
            return RedirectToAction("HomeFriends", "PublicacionFriends");
        }


    }
}

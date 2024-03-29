﻿using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly SubTablaComentarioRepository _subTablaComentarioRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<IdentityUser> _signInManager;


        public PublicacionUsuarioController(IMapper mapper, TablaUsuarioRepository tablaUsuarioRepository,
            SignInManager<IdentityUser> signInManager, TablaPublicacionRepository tablaPublicacionRepository,
            TablaComentarioRepository tablaComentarioRepository, SubTablaComentarioRepository subTablaComentarioRepository)
        {
            
            _mapper = mapper;
            _tablaUsuarioRepository = tablaUsuarioRepository;
            _tablaPublicacionRepository = tablaPublicacionRepository;
            _tablaComentarioRepository = tablaComentarioRepository;
            _signInManager = signInManager;
            _subTablaComentarioRepository = subTablaComentarioRepository;

        }

        public async Task<int> IdUsuarioClienteAsync()
        {
            return await _tablaUsuarioRepository.ReturnIdUsuarioLogueado(User.Identity.Name); ;
        }

        public async Task<IActionResult> Home()
        {
        
            var PublicacionesUsuario = await _tablaPublicacionRepository.TraarPublicacionesMyUsuario(await IdUsuarioClienteAsync());
            var ComentarioUsuarios = await _tablaPublicacionRepository.TraerComentariosMyUsuario();
            var SubComentarioUsuarios = await _tablaPublicacionRepository.TraerSubComentariosMyUsuario();
            var UsuarioActivo = await _tablaUsuarioRepository.GetByIdAsync(await IdUsuarioClienteAsync());
            

            PublicacionUsuarioViewModels pb = new PublicacionUsuarioViewModels();
            pb.ListaComentarioPlantilla = ComentarioUsuarios;
            pb.ListaPublicacionPlantilla = PublicacionesUsuario;
            pb.ListaSubComentarioPlantilla = SubComentarioUsuarios;
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


        [HttpPost]
        public async Task<IActionResult> SubComentar(string comentar, int IdComentario)
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
                return RedirectToAction("Home", "PublicacionUsuario");
            }
            else
            {
                ViewBag.Comentare = "No puedes enviar respuestas vacios...";
                return RedirectToAction("Home", "PublicacionUsuario");
            }

        }

        ///// Ediccion de las publicaciones
        public async Task<IActionResult> EditPb(int? id) {

         
            var publicacion = await _tablaPublicacionRepository.GetPublicacionEdit(id.Value);

            if (publicacion == null)
            {
                return NotFound();
            }


            return View(publicacion);


        }

        [HttpPost]
        public async Task<IActionResult> EditPb(int? Id, PublicacionEditViewModels pvm)
        {

            if (ModelState.IsValid) {


                try
                {

                    var postPublicacion = await _tablaPublicacionRepository.PostPublicacionEdit(pvm, Id.Value);

                                if (postPublicacion) {
                            return RedirectToAction("Home");
                        }
                        
                        ModelState.AddModelError("", "A ocurrido un problema interno en la base de datos.");
                        return View(pvm);

            }

                    catch
            {
                return BadRequest();

            }

        }

            return View(pvm);
        }


        public async Task<IActionResult> DeletePb(int? id) {

            var publicacion = await _tablaPublicacionRepository.GetPublicacionEdit(id.Value);

            if (publicacion == null)
            {
                return NotFound();
            }


            return View(publicacion);


        }

        [HttpPost]
        public async Task<IActionResult> DeletePb(int IdPublicacion)
        {

            
            var publicacion = await _tablaPublicacionRepository.EliminarPublicacionAll(IdPublicacion);

            if (publicacion) {
                return RedirectToAction("Home", "PublicacionUsuario");
            }


            return NotFound();

        }




    }


 }



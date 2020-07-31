using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly UsuarioRepositoryAPI _usuarioRepositoryAPI;
        private readonly IMapper _mapper;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UsuarioController(UsuarioRepositoryAPI usuarioRepositoryAPI, IMapper mapper, SignInManager<IdentityUser> signInManager) {

            _mapper = mapper;
            _usuarioRepositoryAPI = usuarioRepositoryAPI;
            _signInManager = signInManager;
        
        }
        
        [HttpGet]
        public async Task<ActionResult<List<GetUsuarioDTO>>> Get()
        {
            try
            {
                var ListadoUsuario = await _usuarioRepositoryAPI.GetAllUsuarioDTO();

                if (ListadoUsuario == null)
                {
                    return NotFound();
                }

                return ListadoUsuario;
            }
            catch
            {

                return StatusCode(500);
            }

        }

        [HttpGet("{UserName}")]
        public async Task<ActionResult<GetUsuarioDTO>> Get(string UserName)
        {
               try {
                    var Usuario = await _usuarioRepositoryAPI.GetUsuarioDTO(UserName);

                        if (Usuario == null)
                        {
                            return NotFound();
                        }

                        return Usuario;

                        }catch{
                              return StatusCode(500);
                             }
          }


        [HttpGet]
        [Route("AmigosUsuario/{UserName}")]
        public async Task<ActionResult<GetUsuarioAmigosDTO>> Amigos(string UserName)
        {
            try
            {
                var Usuario = await _usuarioRepositoryAPI.GetUsuarioAmigosDTO(UserName);

                if (Usuario == null)
                {
                    return NotFound();
                }

                return Usuario;

             }
            catch
            {
                return StatusCode(500);
            }
         }



        [HttpPost]
        public async Task<ActionResult> Post(PublicarUsuarioDTO publicacion)
        {

            if (ModelState.IsValid)
            {

                var action = await _usuarioRepositoryAPI.UsuarioPublicarDTO(publicacion);

                if (action)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(500);

                }


            }
            return BadRequest();

        }


        [HttpGet]
        [Route("PlusComent/{UserName}")]
        public async Task<ActionResult<GetPublicacionDTO>> PlusComent(string UserName)
        {
            try
            {
                var Usuario = await _usuarioRepositoryAPI.MoreComent(UserName);

                if (Usuario == null)
                {
                    return NotFound();
                }

                return Usuario;

            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpPost]
        [Route("Agregar/{Friends}")]
        public async Task<ActionResult> PostAmigo(string Friends,string Usuario, string Clave)
        {

            if (ModelState.IsValid)
            {


                var action = await _usuarioRepositoryAPI.AddAmigoDTO(Usuario,Clave,Friends);

                if (action)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(500);

                }


            }
            return BadRequest();

        }




        //[HttpGet]
        //[Route("UsuarioAmigo/{UserName}")]
        //public async Task<ActionResult<List<OrderMesaDTO>>> GetOrderTable(string UserName)
        //{

        //    try
        //    {
        //        var OrderMesa = await _mesarepo.GetOrdenMesaByIdDTO(id.Value);

        //        if (OrderMesa == null)
        //        {
        //            return NotFound();
        //        }

        //        return OrderMesa;
        //    }
        //    catch
        //    {
        //        return StatusCode(500);
        //    }

        //}







    }
}

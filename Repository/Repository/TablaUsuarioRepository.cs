using AutoMapper;
using Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Repository.Repository
{
    public class TablaUsuarioRepository : BaseRepository<TablaUsuario, SocialNetworkContext>
    {
        private readonly SocialNetworkContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly TablaAmigoRepository _amigoRepository;
        private readonly TablaComentarioRepository _tablaComentarioRepository;
        private readonly IMapper _mapper;

        public TablaUsuarioRepository(SocialNetworkContext context, UserManager<IdentityUser> userManager,
            IMapper mapper,SignInManager<IdentityUser> signInManager, TablaAmigoRepository amigoRepository,
            TablaComentarioRepository tablaComentarioRepository) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _amigoRepository = amigoRepository;
            _tablaComentarioRepository = tablaComentarioRepository;

        }

        public async Task AddUserIdentity(TablaUsuario identityUser) {

            var user = new IdentityUser { UserName = identityUser.NombreUsuario, Email = identityUser.Correo };
            var result = await _userManager.CreateAsync(user, identityUser.Clave);

        }

        public async Task<bool> VerifyUserIdentiy(LoginViewModels uvs) {

            var result = await _signInManager.PasswordSignInAsync(uvs.NombreUsuario, uvs.Clave, false, true);

            if (result.Succeeded)
            {
                return true;

            }

            return false;
        }




        //Obtener IdUsuario
        public async Task<int> ReturnIdUsuarioLogueado(string nombre) {
            var usuario = await _context.TablaUsuario.FirstOrDefaultAsync(x => x.NombreUsuario == nombre);
            return usuario.IdUsuario;
        }


        public async Task<UsuarioLogueadoPlantilla> UsuarioLogueadoPlantilla(int Id) {

            var usuarioLive = await GetByIdAsync(Id);
           var user = _mapper.Map<UsuarioLogueadoPlantilla>(usuarioLive);
            return user;
        }

        public async Task<List<PlantillaAmigos>> AmigosDelUsuario(int id) {

            var ListadosIntAmigos = await _amigoRepository.ListadoAmigosUsuarioOnline(id);

            var Listado = new List<PlantillaAmigos>();

            foreach (var idAmigo in ListadosIntAmigos) {

               var UserAmigo = await GetByIdAsync(idAmigo);

                var AmigoHecho = _mapper.Map<PlantillaAmigos>(UserAmigo);
                Listado.Add(AmigoHecho);
                
            }

            return Listado;


        }

        public async Task<List<PublicacionPlantilla>> TraerPublicacionAmigos(int id)
        {

            var Amigos = await _amigoRepository.ListadoAmigosUsuarioOnline(id);


            var Listado = new List<PublicacionPlantilla>();

            foreach (var idAmigo in Amigos)
            {
                //Usuario Amigo
                var usuario = await GetByIdAsync(idAmigo);
                var publicaciones = await _context.TablaPublicaciones.Where(op => op.IdUsuario == idAmigo).OrderByDescending(op => op.Fecha).ToListAsync();


                publicaciones.ForEach(op => {

                    var final = _mapper.Map<PublicacionPlantilla>(op);
                    final.Nombre = usuario.Nombre;
                    final.Apellido = usuario.Apellido;
                    final.FotoPerfil = usuario.FotoPerfil;

                    Listado.Add(final);


                });


            }

            return Listado;


        }


        public async Task ActualizarEstado(int id) {

            var usuario = await GetByIdAsync(id);

            if (usuario.Estado == "Inactivo") {

                usuario.Estado = "Activo";
                await UpdateExplicito(usuario);

            }


        
        }

        public async Task<TablaUsuario> VerificarUsuarioEstado(string nombre) {

             var usuario = await _context.TablaUsuario.FirstOrDefaultAsync(x => x.NombreUsuario == nombre);
             return usuario;
        }

        public async Task ActualizarClave(TablaUsuario userd, string clave)
        {
            userd.Clave = clave;
            var user = await _userManager.FindByNameAsync(userd.NombreUsuario);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, clave);
            await UpdateExplicito(userd);

        }

        public async Task<TablaUsuario> ReturnUsuario(string nombre)
        {
            return await _context.TablaUsuario.FirstOrDefaultAsync(x => x.NombreUsuario == nombre);
        }


    }
}

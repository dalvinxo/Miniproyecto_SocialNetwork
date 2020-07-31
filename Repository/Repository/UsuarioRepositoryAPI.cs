using AutoMapper;
using Database.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UsuarioRepositoryAPI : BaseRepository<TablaUsuario, SocialNetworkContext>
    {
        private readonly SocialNetworkContext _context;
        private readonly SubTablaComentarioRepository _subTablaComentarioRepository;
        private readonly TablaUsuarioRepository _tablaUsuarioRepository;
        private readonly TablaAmigoRepository _tablaAmigoRepository;
        private readonly TablaPublicacionRepository _tablaPublicacionRepository;

        private readonly IMapper _mapper;

        public UsuarioRepositoryAPI(SocialNetworkContext context, IMapper mapper,
            SubTablaComentarioRepository subTablaComentarioRepository, TablaAmigoRepository tablaAmigoRepository, TablaUsuarioRepository tablaUsuarioRepository,
            TablaPublicacionRepository tablaPublicacionRepository
            ) : base(context)
        {
            _context = context;
            _mapper = mapper;
            //_amigoRepository = amigoRepository;
            //_tablaComentarioRepository = tablaComentarioRepository;
            _tablaPublicacionRepository = tablaPublicacionRepository;
            _subTablaComentarioRepository = subTablaComentarioRepository;
            _tablaUsuarioRepository = tablaUsuarioRepository;
            _tablaAmigoRepository = tablaAmigoRepository;

        }


        public async Task<List<GetUsuarioDTO>> GetAllUsuarioDTO() {

            var UserAll = await GetAllAsync();

            var ListadoUsuario = new List<GetUsuarioDTO>();

            foreach (var usuario in UserAll) {

                var UsuarioGetDto = _mapper.Map<GetUsuarioDTO>(usuario);
                UsuarioGetDto.ListPublicacionDTO = await GetPublicacionDTO(usuario.IdUsuario);

                ListadoUsuario.Add(UsuarioGetDto);
            }

            return ListadoUsuario;

        }

        //usuario especifico
        public async Task<GetUsuarioDTO> GetUsuarioDTO(string name)
        {

            var UserEspecific = await _tablaUsuarioRepository.ReturnUsuario(name);

            if (UserEspecific == null) {
                return null;
            }
            
            var UsuarioGetDto = _mapper.Map<GetUsuarioDTO>(UserEspecific);
            UsuarioGetDto.ListPublicacionDTO = await GetPublicacionDTO(UserEspecific.IdUsuario);            
            return UsuarioGetDto;

        }

        //amigos de un usuario
        public async Task<GetUsuarioAmigosDTO> GetUsuarioAmigosDTO(string name)
        {

            var UserEspecific = await _tablaUsuarioRepository.ReturnUsuario(name);

            if (UserEspecific == null)
            {
                return null;
            }

            var userd = _mapper.Map<GetUsuarioAmigosDTO>(UserEspecific);
            userd.ListaAmigosUsuario = await GetListAmigosDTO(UserEspecific.IdUsuario);
            return userd;

        }

        //Metodo lista de amigos usuario
        public async Task<List<AmigosDTO>> GetListAmigosDTO(int IdUsuario)
        {

            var ListadoIntAmigo =  await _tablaAmigoRepository.ListadoAmigosUsuarioOnline(IdUsuario);

            var ListaAmigoDTO = new List<AmigosDTO>();

           foreach (var amigo in ListadoIntAmigo) {

                var amigoFull = await GetByIdAsync(amigo);
                var SuccesAmigo = _mapper.Map<AmigosDTO>(amigoFull);
                ListaAmigoDTO.Add(SuccesAmigo);
            }

            return ListaAmigoDTO;

       
        }


        //Publicar una publicacion -_-
        public async Task<bool> UsuarioPublicarDTO(PublicarUsuarioDTO pud) {

            try
            {
                var verify = await _context.TablaUsuario.FirstOrDefaultAsync(x => x.NombreUsuario == pud.NombreUsuario && x.Clave == pud.Clave);

                if (verify == null)
                {

                    return false;

                }


                var publicacion = _mapper.Map<TablaPublicaciones>(pud);
                publicacion.FotoPublicacion = "none.png";
                publicacion.IdUsuario = verify.IdUsuario;
                await _tablaPublicacionRepository.AddAsync(publicacion);
                return true;

            }catch {
                return false;
            }

        }


        //Publicacion con mas comentario
        public async Task<GetPublicacionDTO> MoreComent(string name) {

            var UserEspecific = await _tablaUsuarioRepository.ReturnUsuario(name);

            if (UserEspecific == null)
            {
                return null;
            }

            return await GetMorePublicacionDTO(UserEspecific.IdUsuario);
        }


        //Logica Publicacion
        public async Task<GetPublicacionDTO> GetMorePublicacionDTO(int IdUsuario)
        {

            //traer todas las publicacion
            var Publicaciones = await _context.TablaPublicaciones.Where(ok => ok.IdUsuario == IdUsuario).ToListAsync();
            
            //instancia publicacion 
            var publicacionEvaluar = new GetPublicacionDTO();

            var ComentarioEvaluar = new List<GetComentariosDTO>();

            int post = 0;
            int cont = 0;
            var publicacionM = new GetPublicacionDTO();
            foreach (var publicacion in Publicaciones)
            {
               
                var ListComentariosDTO = await GetComentariosDTO(publicacion.IdPublicacion);
                post = ListComentariosDTO.Count();


                if (cont < post) {

                    cont = post;
                    publicacionM.IdPublicacion = publicacion.IdPublicacion;
                }


            }

           var publicacionPlus = await _context.TablaPublicaciones.FirstOrDefaultAsync(x => x.IdPublicacion == publicacionM.IdPublicacion);
            var getPublicacionDTO = _mapper.Map<GetPublicacionDTO>(publicacionPlus);
            getPublicacionDTO.ListComentariosDTO = await GetComentariosDTO(publicacionM.IdPublicacion);
            return getPublicacionDTO;

        }


        //AgregarAmigos
        public async Task<bool> AddAmigoDTO(string UsuarioIdUser, string clave, string FriendsIdUser) {

            try { 
            var verify = await _context.TablaUsuario.FirstOrDefaultAsync(x => x.NombreUsuario == UsuarioIdUser && x.Clave == clave);
            var Friends = await _context.TablaUsuario.FirstOrDefaultAsync(x => x.NombreUsuario == FriendsIdUser);
            if (verify == null)
            {

                return false;

            }

            if (Friends== null) {
                return false;
            }

            int IdUsuarioUser = await _tablaUsuarioRepository.ReturnIdUsuarioLogueado(UsuarioIdUser);

            var Tad = new TablaAmigo();
            Tad.FriendsIdUsuario = Friends.IdUsuario;
            Tad.UserIdUsuario = IdUsuarioUser;

            var exists = await _context.TablaAmigo.FirstOrDefaultAsync(op => op.UserIdUsuario == Tad.UserIdUsuario && op.FriendsIdUsuario == Tad.FriendsIdUsuario);

            if (exists != null) {

                return true;
            }

            await _tablaAmigoRepository.AddAsync(Tad);

            return true;

            }
            catch {
                return false;
            }

        }




        //Metodo lista de publicaciones
        public async Task<List<GetPublicacionDTO>> GetPublicacionDTO(int IdUsuario)
        {

            var Publicaciones = await _context.TablaPublicaciones.Where(ok => ok.IdUsuario == IdUsuario).ToListAsync();
            var ListadoComentario = new List<GetPublicacionDTO>();

            foreach (var publicacion in Publicaciones) {

                var getPublicacionDTO = _mapper.Map<GetPublicacionDTO>(publicacion);
                getPublicacionDTO.ListComentariosDTO = await GetComentariosDTO(publicacion.IdPublicacion);

                ListadoComentario.Add(getPublicacionDTO);
            }

            return ListadoComentario;
        }


        public async Task<List<GetComentariosDTO>> GetComentariosDTO(int IdPublicacion) {

            var Comentarios = await _context.TablaComentarios.Where(ok => ok.IdPublicacion == IdPublicacion).ToListAsync();
        
            var ListadoComentario = new List<GetComentariosDTO>();

                
            foreach(var comentario in Comentarios) { 

                var getComentariosDTO = _mapper.Map<GetComentariosDTO>(comentario);
                getComentariosDTO.ListSubComentariosDTO = await _subTablaComentarioRepository.ListadoGetSubComentariosDTO(comentario.IdComentario);

                ListadoComentario.Add(getComentariosDTO);

            }

            return ListadoComentario;
        }


    }
}

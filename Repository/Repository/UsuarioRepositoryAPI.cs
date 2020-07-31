using AutoMapper;
using Database.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
using Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

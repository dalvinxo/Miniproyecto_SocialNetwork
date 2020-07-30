using AutoMapper;
using Database.Models;
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
    public class TablaAmigoRepository : BaseRepository<TablaAmigo, SocialNetworkContext>
    {
        private readonly SocialNetworkContext _context;
        private readonly IMapper _mapper;

        public TablaAmigoRepository(IMapper mapper,SocialNetworkContext context) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<List<int>> ListadoAmigosUsuarioOnline(int Id) 
        {  
            return await _context.TablaAmigo.Where(x => x.UserIdUsuario == Id).Select(x => x.FriendsIdUsuario).ToListAsync();                 
       
        }

        public async Task<bool> AddAmigos(AgregarUsuarioViewModels ok){

            var amigo = await _context.TablaUsuario.FirstOrDefaultAsync(x => x.NombreUsuario == ok.NombreUsuario);

            if (amigo != null)
            {
                var NuevosAmigos = new AmigoViewModels();
                NuevosAmigos.FriendsIdUsuario = amigo.IdUsuario;
                NuevosAmigos.UserIdUsuario = ok.UserIdUsuario;
                var Full = _mapper.Map<TablaAmigo>(NuevosAmigos);
                await AddAsync(Full);


                return true;
            }

            return false;


        }



    }
}

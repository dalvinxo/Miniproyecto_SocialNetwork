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
    public class SubTablaComentarioRepository : BaseRepository<SubTablaComentarios, SocialNetworkContext>
    {
        private readonly SocialNetworkContext _context;
        private readonly IMapper _mapper;

        public SubTablaComentarioRepository(SocialNetworkContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<SubComentariosDTO>> ListadoGetSubComentariosDTO(int IdComentario) {

            var SubComentarios =await _context.SubTablaComentarios.Where(op => op.IdComentario == IdComentario).ToListAsync();

            var SubComentariosDTO = new List<SubComentariosDTO>();

            SubComentarios.ForEach(op =>
            {

                var subComentarios = _mapper.Map<SubComentariosDTO>(op);
                SubComentariosDTO.Add(subComentarios);
            });

            return SubComentariosDTO;
        
        }

    }
}

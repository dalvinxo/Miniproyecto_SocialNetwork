using Database.Models;
using Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class SubTablaComentarioRepository : BaseRepository<SubTablaComentarios, SocialNetworkContext>
    {
        private readonly SocialNetworkContext _context;

        public SubTablaComentarioRepository(SocialNetworkContext context) : base(context)
        {
            _context = context;
        }

    }
}

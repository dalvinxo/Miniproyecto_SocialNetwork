using Database.Models;
using Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class TablaComentarioRepository : BaseRepository<TablaComentarios, SocialNetworkContext>
    {
        private readonly SocialNetworkContext _context;

        public TablaComentarioRepository(SocialNetworkContext context) : base(context)
        {
            _context = context;
        }

    }
}

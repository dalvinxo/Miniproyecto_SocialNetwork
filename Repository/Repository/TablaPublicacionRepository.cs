using Database.Models;
using Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
   public class TablaPublicacionRepository : BaseRepository<TablaPublicaciones, SocialNetworkContext>
    {
        private readonly SocialNetworkContext _context;

        public TablaPublicacionRepository(SocialNetworkContext context) : base(context)
        {
            _context = context;
        }

    }
}

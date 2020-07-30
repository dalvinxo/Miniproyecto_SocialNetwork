using Database.Models;
using Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class TablaAmigoRepository : BaseRepository<TablaAmigo, SocialNetworkContext>
    {
        private readonly SocialNetworkContext _context;

        public TablaAmigoRepository(SocialNetworkContext context) : base(context)
        {
            _context = context;
        }
    }
}

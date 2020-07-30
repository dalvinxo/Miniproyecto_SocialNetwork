using Database.Models;
using Microsoft.AspNetCore.Identity;
using Repository.BaseRepository;
using System;
using System.Collections.Generic;
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

        public TablaUsuarioRepository(SocialNetworkContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

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


    }
}

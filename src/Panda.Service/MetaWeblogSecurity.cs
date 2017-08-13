using System.Threading;
using System.Threading.Tasks;
using cloudscribe.MetaWeblog;
using Microsoft.AspNetCore.Identity;
using Panda.Core.Models.Data;

namespace Panda.Service
{
    public class MetaWeblogSecurity : IMetaWeblogSecurity
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public MetaWeblogSecurity(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<MetaWeblogSecurityResult> ValiatePermissions(MetaWeblogRequest request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, true, false).ConfigureAwait(false);

            if (result.Succeeded)
            {
                // get blog info
                return new MetaWeblogSecurityResult("Default Blog", "1", true, true, false);

            }

            return new MetaWeblogSecurityResult("", "", false, false, false);
        }
    }
}

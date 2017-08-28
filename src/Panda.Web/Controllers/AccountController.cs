using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Panda.Core.Models.Data;
using Panda.Core.Models.Request;
using Panda.Core.Models.View;

namespace Panda.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<LoginResultViewModel> Login([FromBody]LoginViewModel model)
        {
            var response = new LoginResultViewModel();
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    var user = await _userManager.FindByNameAsync(model.Username);
                    response.Username = user.UserName;
                    response.DisplayName = user.DisplayName;
                    response.Email = user.Email;
                    response.Succeeded = true;
                }
                else
                {
                    response.Succeeded = false;
                }
            }
            return response;
        }

        [HttpGet]
        [Route("IsLoggedIn")]
        public async Task<LoginResultViewModel> IsLoggedIn()
        {
            var claimsPrincipal = HttpContext.User;
            var user = await _userManager.GetUserAsync(claimsPrincipal);

            if (user != null)
            {
                return new LoginResultViewModel
                {
                    Username = user.UserName,
                    DisplayName = user.DisplayName,
                    Succeeded = true
                };
            }

            return new LoginResultViewModel
            {
                Succeeded = false
            };
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<bool> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return true;
        }

        [Authorize]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<PasswordChangeResult> ChangePassword([FromBody]PasswordChangeRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword)
                .ConfigureAwait(false);

            return new PasswordChangeResult
            {
                Succeeded = result.Succeeded,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }
    }
}
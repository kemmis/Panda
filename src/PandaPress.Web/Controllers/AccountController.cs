using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PandaPress.Core.Models.Data;
using PandaPress.Core.Models.View;

namespace PandaPress.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
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
                    response.Succeeded = true;
                }
                else
                {
                    response.Succeeded = false;
                }
            }
            return response;
        }
    }
}
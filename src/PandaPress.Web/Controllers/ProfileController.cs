using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PandaPress.Core.Contracts;
using PandaPress.Core.Models.Data;
using PandaPress.Core.Models.Request;
using PandaPress.Core.Models.View;

namespace PandaPress.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Profile")]
    public class ProfileController : Controller
    {
        private readonly IPostService _postService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(IPostService postService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        [Route("GetProfileSettings")]
        [HttpGet]
        public async Task<ProfileSettingsViewModel> GetProfileSettings()
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAwait(false);
            return _postService.GetProfileSettings(user.Id);
        }

        [Route("SaveProfileSettings")]
        [HttpPost]
        public async Task<ProfileSettingsViewModel> SaveProfileSettings([FromBody]ProfileSettingsUpdateRequest request)
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAwait(false);
            return _postService.UpdateProfileSettings(user.Id, request);
        }
    }
}
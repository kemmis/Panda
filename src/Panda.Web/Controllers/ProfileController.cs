﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panda.Core.Contracts;
using Panda.Core.Models.Data;
using Panda.Core.Models.Request;
using Panda.Core.Models.View;

namespace Panda.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Profile")]
    public class ProfileController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(IBlogService blogService, UserManager<ApplicationUser> userManager)
        {
            _blogService = blogService;
            _userManager = userManager;
        }

        [Route("GetProfileSettings")]
        [HttpGet]
        public async Task<ProfileSettingsViewModel> GetProfileSettings()
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAwait(false);
            return _blogService.GetProfileSettings(user.Id);
        }

        [Route("SaveProfileSettings")]
        [HttpPost]
        public async Task<ProfileSettingsViewModel> SaveProfileSettings([FromBody]ProfileSettingsUpdateRequest request)
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAwait(false);
            return _blogService.UpdateProfileSettings(user.Id, request);
        }

        [Route("SavePhoto")]
        [HttpPost]
        public async Task<ProfileSettingsViewModel> SavePhoto(IFormFile file)
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAwait(false);
            return await _blogService.SaveProfilePicture(user.Id, file).ConfigureAwait(false);
        }

        [Route("RemovePhoto")]
        [HttpGet]
        public async Task<ProfileSettingsViewModel> RemovePhoto()
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAwait(false);
            return _blogService.RemoveProfilePhoto(user.Id);    
        }
    }
}
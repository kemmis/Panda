using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandaPress.Core.Contracts;
using PandaPress.Core.Models.View;

namespace PandaPress.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Settings")]
    public class SettingsController : Controller
    {
        private readonly IPostService _postService;

        public SettingsController(IPostService postService)
        {
            _postService = postService;
        }

        [Route("Get")]
        [HttpGet]
        public SettingsViewModel Get()
        {
            return _postService.GetBlogSettings();
        }
        [Route("Save")]
        [HttpPost]
        public SettingsViewModel Save([FromBody]SettingsViewModel settings)
        {
            return _postService.SaveBlogSettings(settings);
        }
    }
}
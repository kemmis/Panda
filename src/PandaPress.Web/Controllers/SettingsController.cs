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
        private readonly IBlogService _blogService;

        public SettingsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [Route("Get")]
        [HttpGet]
        public SettingsViewModel Get()
        {
            return _blogService.GetBlogSettings();
        }
        [Route("Save")]
        [HttpPost]
        public SettingsViewModel Save([FromBody]SettingsViewModel settings)
        {
            return _blogService.SaveBlogSettings(settings);
        }
    }
}
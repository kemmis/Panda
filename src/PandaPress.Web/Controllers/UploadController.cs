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
    [Route("api/Upload")]
    public class UploadController : Controller
    {
        private readonly IBlogService _blogService;

        public UploadController(IBlogService blogService)
        {
            _blogService = blogService;
        }
            
        [HttpPost]
        [Route("")]
        public async Task<MediaViewModel> Index(IFormFile file)
        {
            return await _blogService.UploadMedia(file);
        }
    }
}
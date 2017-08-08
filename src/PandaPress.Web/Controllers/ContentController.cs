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
    [Route("api/Content")]
    public class ContentController : Controller
    {
        private readonly IPostService _postService;

        public ContentController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        [Route("GetContent")]
        public ContentViewModel GetContent()
        {
            return _postService.GetContent();
        }
    }
}
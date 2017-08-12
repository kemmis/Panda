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
        private readonly IBlogService _blogService;

        public ContentController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        [Route("GetContent")]
        public ContentViewModel GetContent()
        {
            return _blogService.GetContent();
        }

        [HttpGet]
        [Route("DeletePost")]
        public void DeletePost(int postId)
        {
            _blogService.DeletePost(postId);
        }

        [HttpGet]
        [Route("UnDeletePost")]
        public void UnDeletePost(int postId)
        {
            _blogService.UnDeletePost(postId);
        }
    }
}
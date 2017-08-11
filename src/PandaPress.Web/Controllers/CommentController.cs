using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandaPress.Core.Contracts;
using PandaPress.Core.Models.Request;
using PandaPress.Core.Models.View;

namespace PandaPress.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Comment")]
    public class CommentController : Controller
    {
        private readonly IPostService _postService;

        public CommentController(IPostService postService)
        {
            _postService = postService;
        }

        [Route("Save")]
        [HttpPost]
        public CommentViewModel Save([FromBody]CommentCreateRequest request)
        {
            return _postService.SaveComment(request);
        }
    }
}
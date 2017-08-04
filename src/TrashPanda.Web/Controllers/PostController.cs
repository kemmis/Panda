using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrashPanda.Core.Models.View;
using TrashPanda.Core.Contracts;
using PandaPress.Core.Models.View;
using PandaPress.Core.Models.Request;

namespace TrashPanda.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Post")]
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [Route("GetBySlug")]
        public PostViewModel GetBySlug(string slug)
        {
            return postService.GetPostBySlug(slug);
        }

        [Route("GetList")]
        public PostListViewModel GetList([FromBody]PostListRequest request)
        {
            return postService.GetPostList(request);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using PandaPress.Core.Contracts;
using PandaPress.Core.Models.Request;
using PandaPress.Core.Models.View;

namespace PandaPress.Web.Controllers
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
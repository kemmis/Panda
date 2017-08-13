using Microsoft.AspNetCore.Mvc;
using Panda.Core.Contracts;
using Panda.Core.Models.Request;
using Panda.Core.Models.View;

namespace Panda.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Post")]
    public class PostController : Controller
    {
        private readonly IBlogService _blogService;

        public PostController(IBlogService blogService)
        {
            this._blogService = blogService;
        }

        [HttpGet]
        [Route("GetBySlug")]
        public PostViewModel GetBySlug(string slug)
        {
            return _blogService.GetPostBySlug(slug);
        }

        [HttpPost]
        [Route("GetList")]
        public PostListViewModel GetList([FromBody]PostListRequest request)
        {
            return _blogService.GetPostList(request);
        }

        [HttpPost]
        [Route("GetCategoryList")]
        public PostListViewModel GetCategoryList([FromBody]PostListRequest request)
        {
            return _blogService.GetPostCategoryList(request);
        }
    }
}
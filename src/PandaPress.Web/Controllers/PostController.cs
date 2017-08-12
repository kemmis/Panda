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
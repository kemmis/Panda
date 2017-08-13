using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Core.Contracts;
using Panda.Core.Models.View;

namespace Panda.Web.Controllers
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
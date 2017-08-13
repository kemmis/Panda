using Microsoft.AspNetCore.Mvc;
using Panda.Core.Contracts;
using Panda.Core.Models.Request;
using Panda.Core.Models.View;

namespace Panda.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Comment")]
    public class CommentController : Controller
    {
        private readonly IBlogService _blogService;

        public CommentController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [Route("Save")]
        [HttpPost]
        public CommentViewModel Save([FromBody]CommentCreateRequest request)
        {
            return _blogService.SaveComment(request);
        }
    }
}
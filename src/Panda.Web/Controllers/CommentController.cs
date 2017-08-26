using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Core.Contracts;
using Panda.Core.Models.Request;
using Panda.Core.Models.View;
using System.Threading.Tasks;

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
        public async Task<CommentViewModel> Save([FromBody]CommentCreateRequest request)
        {
            return await _blogService.SaveComment(request);
        }

        [Authorize]
        [Route("Delete")]
        [HttpGet]
        public CommentViewModel Delete(int commentId)
        {
            return _blogService.DeleteComment(commentId);
        }

        [Authorize]
        [Route("UnDelete")]
        [HttpGet]
        public CommentViewModel UnDelete(int commentId)
        {
            return _blogService.UnDeleteComment(commentId);
        }
    }
}
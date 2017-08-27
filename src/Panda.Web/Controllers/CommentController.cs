using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panda.Core.Contracts;
using Panda.Core.Models.Data;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(IBlogService blogService, UserManager<ApplicationUser> userManager)
        {
            _blogService = blogService;
            _userManager = userManager;
        }

        [Route("Save")]
        [HttpPost]
        public async Task<CommentViewModel> Save([FromBody]CommentCreateRequest request)
        {
            var claimsPrincipal = HttpContext.User;
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            return await _blogService.SaveComment(request, user != null);
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
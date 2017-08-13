using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panda.Core.Contracts;
using Panda.Core.Models.Data;
using Panda.Core.Models.View;

namespace Panda.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/PostEditor")]
    public class PostEditorController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostEditorController(IBlogService blogService, UserManager<ApplicationUser> userManager)
        {
            _blogService = blogService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetById")]
        public EditPostViewModel GetById(int postId)
        {
            return _blogService.GetPostToEdit(postId);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<EditPostViewModel> Save([FromBody]EditPostViewModel post)
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAwait(false);
            return _blogService.SavePost(post, user.UserName);
        }
    }
}
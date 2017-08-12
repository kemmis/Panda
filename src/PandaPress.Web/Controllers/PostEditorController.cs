using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PandaPress.Core.Contracts;
using PandaPress.Core.Models.Data;
using PandaPress.Core.Models.View;

namespace PandaPress.Web.Controllers
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
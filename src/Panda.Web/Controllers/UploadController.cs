using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Panda.Core.Contracts;
using Panda.Core.Models.View;

namespace Panda.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Upload")]
    public class UploadController : Controller
    {
        private readonly IBlogService _blogService;

        public UploadController(IBlogService blogService)
        {
            _blogService = blogService;
        }
            
        [HttpPost]
        [Route("")]
        public async Task<MediaViewModel> Index(IFormFile file)
        {
            return await _blogService.UploadMedia(file);
        }
    }
}
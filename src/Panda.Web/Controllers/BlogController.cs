using Microsoft.AspNetCore.Mvc;
using Panda.Core.Contracts;
using Panda.Core.Models.View;

namespace Panda.Web.Controllers
{
  
    [Produces("application/json")]
    [Route("api/Blog")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [Route("GetInfo")]
        [HttpGet]
        public HomeViewModel GetInfo()
        {
            return _blogService.GetHomeData();
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Core.Contracts;
using Panda.Core.Models.View;

namespace Panda.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Dashboard")]
    public class DashboardController : Controller
    {
        private readonly IBlogService _blogService;

        public DashboardController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [Route("GetData")]
        public DashboardDataViewModel GetData()
        {
            return _blogService.GetDashboardData();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandaPress.Core.Contracts;
using PandaPress.Core.Models.View;

namespace PandaPress.Web.Controllers
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
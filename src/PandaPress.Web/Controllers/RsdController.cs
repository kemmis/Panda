using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PandaPress.Core.Models.View;
using System.Reflection;

namespace PandaPress.Web.Controllers
{
    public class RsdController : Controller
    {
        public IActionResult Index()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var metaWeblogApiUri = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/api/metaweblog";
            var blogUri = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

            var viewModel = new RsdViewModel
            {
                EngineName = "PandaPress",
                EngineVersion = version,
                MetaWeblogApiUri = metaWeblogApiUri,
                BlogHomePageUri = blogUri,
                BlogId = blogUri,
                EngineLink = "https://github.com/kemmis/PandaPress"
            };

            Response.ContentType = "text/xml";
            return View(viewModel);
        }
    }
}
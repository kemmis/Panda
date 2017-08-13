using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Panda.Core.Models.View;

namespace Panda.Web.Controllers
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
                EngineName = "Panda",
                EngineVersion = version,
                MetaWeblogApiUri = metaWeblogApiUri,
                BlogHomePageUri = blogUri,
                BlogId = "1",
                EngineLink = "https://github.com/kemmis/Panda"
            };

            Response.ContentType = "text/xml";
            return View(viewModel);
        }
    }
}
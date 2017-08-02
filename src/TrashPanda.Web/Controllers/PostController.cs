using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrashPanda.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Post")]
    public class PostController : Controller
    {
        [Route("GetBySlug")]
        public PostViewModel GetBySlug(string slug)
        {
            return new PostViewModel
            {
                Title = "Node Blows My Mind"
            };
        }
    }

    public class PostViewModel
    {
        public string Title { get; set; }
    }
}
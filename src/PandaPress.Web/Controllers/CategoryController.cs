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
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly IPostService _postService;

        public CategoryController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        [Route("Add")]
        public CategoryContentViewModel Add(string title, string description)
        {
            return _postService.AddCategory(title, description);
        }

        [HttpGet]
        [Route("Delete")]
        public void Delete(int categoryId)
        {
            _postService.DeleteCategory(categoryId);
        }
    }
}
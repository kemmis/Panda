using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Core.Contracts;
using Panda.Core.Models.View;

namespace Panda.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly IBlogService _blogService;

        public CategoryController(IBlogService blogService)
        {
            _blogService = blogService;
        }


        [HttpGet]
        [Route("GetAll")]
        public List<CategoryContentViewModel> GetAll()
        {
            return _blogService.GetAllCategories();
        }


        [HttpGet]
        [Route("Add")]
        public CategoryContentViewModel Add(string title, string description)
        {
            return _blogService.AddCategory(title, description);
        }

        [HttpGet]
        [Route("Delete")]
        public void Delete(int categoryId)
        {
            _blogService.DeleteCategory(categoryId);
        }
    }
}
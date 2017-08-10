using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PandaPress.Core.Contracts;
using PandaPress.Core.Models.Data;
using PandaPress.Core.Models.Request;
using PandaPress.Core.Models.View;

namespace PandaPress.Service
{
    public class PostService : IPostService
    {
        private readonly IPandaPressDataProvider _dataProvider;
        private readonly IMapper _mapper;
        private readonly IMediaStorageService _mediaStorageService;
        private readonly ISlugService _slugService;

        public PostService(IPandaPressDataProvider dataProvider, IMapper mapper, IMediaStorageService mediaStorageService, ISlugService slugService)
        {
            _dataProvider = dataProvider;
            _mapper = mapper;
            _mediaStorageService = mediaStorageService;
            _slugService = slugService;
        }

        public void DeletePost(string blogId, string postId)
        {
            throw new System.NotImplementedException();
        }

        public void EditPost(PostEditRequest request)
        {
            _dataProvider.UpdatePost(request.PostId, request.Title, request.Content, request.Publish);
        }

        public List<Blog> GetBlogsForUser(string username)
        {
            var blogs = _dataProvider.GetBlogsForUser(username);
            return blogs.ToList();
        }

        public IEnumerable<PostEditRequest> GetCategories(string blogId, string userName)
        {
            throw new System.NotImplementedException();
        }

        public List<Category> GetCategories()
        {
            return _dataProvider.GetCategories().ToList();
        }

        public PostViewModel GetPost(string postId)
        {
            var post = _dataProvider.GetPostById(int.Parse(postId));
            if (post == null) return null;
            return _mapper.Map<PostViewModel>(post);
        }

        public PostViewModel GetPostBySlug(string slug)
        {
            var post = _dataProvider.GetPostBySlug(slug);

            if (post == null) return null;

            return _mapper.Map<PostViewModel>(post);
        }

        public PostListViewModel GetPostList(PostListRequest request)
        {
            var posts = _dataProvider.GetPosts(request.PageSize, request.PageIndex);
            return new PostListViewModel
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Posts = _mapper.Map<List<PostViewModel>>(posts.posts),
                TotalPosts = posts.totalPosts
            };
        }

        public IEnumerable<PostViewModel> GetRecentPosts(string blogId)
        {
            throw new System.NotImplementedException();
        }

        public void NewCategory(string blogId, string category)
        {
            throw new System.NotImplementedException();
        }

        public void NewMediaObject(string blogId)
        {
            throw new System.NotImplementedException();
        }

        public Post NewPost(PostCreateRequest request)
        {
            var slug = _slugService.CreateSlugFromTitle(request.Title);
            return _dataProvider.CreatePost(request.Title, request.Content, slug, request.Username, request.Publish, request.BlogId);
        }

        public async Task<string> SaveMedia(byte[] bytes, string name)
        {
            return await _mediaStorageService.SaveMedia("/blog-media/", name, bytes).ConfigureAwait(false);
        }

        public SettingsViewModel GetBlogSettings()
        {
            var blog = _dataProvider.GetBlog();
            return new SettingsViewModel()
            {
                BlogName = blog.Name,
                BlogId = blog.Id
            };
        }

        public SettingsViewModel SaveBlogSettings(SettingsViewModel settings)
        {
            _dataProvider.UpdateBlog(settings.BlogId, settings.BlogName);
            return GetBlogSettings();
        }

        public DashboardDataViewModel GetDashboardData()
        {
            return new DashboardDataViewModel
            {
                NumDrafts = _dataProvider.GetNumDrafts(),
                NumPosts = _dataProvider.GetNumPublishedPosts()
            };
        }

        public ContentViewModel GetContent()
        {
            var posts = _dataProvider.GetPosts();
            var categories = _dataProvider.GetCategoriesWithPostCategories();

            return new ContentViewModel
            {
                Posts = posts.Select(p => new PostContentViewModel
                {
                    Published = p.Published,
                    Id = p.Id,
                    Title = p.Title
                }).ToList(),
                Categories = categories.Select(c => new CategoryContentViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    NumPosts = c.PostCategories.Count,
                    Description = c.Description
                }).ToList()
            };
        }

        public CategoryContentViewModel AddCategory(string title, string description)
        {
            var category = _dataProvider.AddCategory(title, description);
            return new CategoryContentViewModel
            {
                Id = category.Id,
                Title = category.Title,
                NumPosts = category.PostCategories.Count,
                Description = category.Description
            };
        }

        public void DeleteCategory(int categoryId)
        {
            _dataProvider.DeleteCategory(categoryId);
        }
    }
}

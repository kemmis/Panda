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
    public class BlogService : IBlogService
    {
        private readonly IPandaPressDataProvider _dataProvider;
        private readonly IMapper _mapper;
        private readonly IMediaStorageService _mediaStorageService;
        private readonly ISlugService _slugService;

        public BlogService(IPandaPressDataProvider dataProvider, IMapper mapper, IMediaStorageService mediaStorageService, ISlugService slugService)
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
            _dataProvider.UpdatePost(request.PostId, request.Title, request.Content, request.Categories, request.Publish);
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

        public PostListViewModel GetPostCategoryList(PostListRequest request)
        {
            var posts = _dataProvider.GetPostsByCategorySlug(request.PageSize, request.PageIndex, request.CategorySlug);
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
            var slug = _slugService.CreateSlugFromPostTitle(request.Title);
            return _dataProvider.CreatePost(request.Title, request.Content, request.Categories, slug, request.Username, request.Publish, request.BlogId);
        }

        public async Task<string> SaveMedia(byte[] bytes, string name)
        {
            return await _mediaStorageService.SaveMedia("/blog-media/", name, bytes).ConfigureAwait(false);
        }

        public SettingsViewModel GetBlogSettings()
        {
            var blog = _dataProvider.GetBlog();
            return _mapper.Map<SettingsViewModel>(blog);
        }

        public SettingsViewModel SaveBlogSettings(SettingsViewModel settings)
        {
            _dataProvider.UpdateBlog(settings.BlogId, settings.BlogName, settings.Description, settings.PostsPerPage);
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

            var postList = _mapper.Map<List<PostContentViewModel>>(posts);
            var catList = _mapper.Map<List<CategoryContentViewModel>>(categories);

            return new ContentViewModel
            {
                Categories = catList,
                Posts = postList
            };
        }

        public CategoryContentViewModel AddCategory(string title, string description)
        {
            var slug = _slugService.CreateSlugFromPostTitle(title);
            var category = _dataProvider.AddCategory(title, description, slug);

            return new CategoryContentViewModel
            {
                Id = category.Id,
                Title = category.Title,
                NumPosts = category.PostCategories.Count,
                Description = category.Description,
                Slug = category.Slug
            };
        }

        public void DeleteCategory(int categoryId)
        {
            _dataProvider.DeleteCategory(categoryId);
        }

        public ProfileSettingsViewModel GetProfileSettings(string userId)
        {
            var user = _dataProvider.GetUserById(userId);
            return _mapper.Map<ProfileSettingsViewModel>(user);
        }

        public ProfileSettingsViewModel UpdateProfileSettings(string userId, ProfileSettingsUpdateRequest request)
        {
            var user = _dataProvider.UpdateUser(userId, request.DisplayName, request.About);
            return _mapper.Map<ProfileSettingsViewModel>(user);
        }

        public CommentViewModel SaveComment(CommentCreateRequest request)
        {
            var comment =
                _dataProvider.CreateComment(request.PostId, request.AuthorName, request.AuthorEmail, request.Text);
            return _mapper.Map<CommentViewModel>(comment);
        }

        public HomeViewModel GetHomeData()
        {
            var blog = _dataProvider.GetBlog();
            return _mapper.Map<HomeViewModel>(blog);
        }

        public void DeletePost(int postId)
        {
            _dataProvider.DeletePost(postId);
        }

        public void UnDeletePost(int postId)
        {
            _dataProvider.UnDeletePost(postId);
        }
    }
}

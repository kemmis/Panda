using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Panda.Core.Contracts;
using Panda.Core.Models.Data;
using Panda.Core.Models.Request;
using Panda.Core.Models.View;

namespace Panda.Service
{
    public class BlogService : IBlogService
    {
        private readonly IPandaDataProvider _dataProvider;
        private readonly IMapper _mapper;
        private readonly IMediaStorageService _mediaStorageService;
        private readonly ISlugService _slugService;

        public BlogService(IPandaDataProvider dataProvider, IMapper mapper, IMediaStorageService mediaStorageService, ISlugService slugService)
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

        public List<CategoryContentViewModel> GetAllCategories()
        {
            var categories = _dataProvider.GetCategories().ToList();
            return _mapper.Map<List<CategoryContentViewModel>>(categories);
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
            var blog = _dataProvider.GetBlog();
            var posts = _dataProvider.GetPosts(blog.PostsPerPage, request.PageIndex);
            return new PostListViewModel
            {
                PageTitle = blog.Name + " - " + blog.Description,
                PageSize = blog.PostsPerPage,
                PageIndex = request.PageIndex,
                Posts = _mapper.Map<List<PostViewModel>>(posts.posts),
                TotalPosts = posts.totalPosts
            };
        }

        public PostListViewModel GetPostCategoryList(PostListRequest request)
        {
            var blog = _dataProvider.GetBlog();
            var posts = _dataProvider.GetPostsByCategorySlug(blog.PostsPerPage, request.PageIndex, request.CategorySlug);
            return new PostListViewModel
            {
                PageTitle = blog.Name + " - " + blog.Description,
                PageSize = blog.PostsPerPage,
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

        public async Task<ProfileSettingsViewModel> SaveProfilePicture(string userId, IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var bytes = ms.ToArray();
                var path = await _mediaStorageService.SaveMedia("/blog-media/profile/", file.FileName, bytes);
                _dataProvider.SaveProfilePicture(userId, path);
                return GetProfileSettings(userId);
            }
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
            var recentComments = _dataProvider.GetRecentComments();

            return new DashboardDataViewModel
            {
                NumDrafts = _dataProvider.GetNumDrafts(),
                NumPosts = _dataProvider.GetNumPublishedPosts(),
                RecentComments = _mapper.Map<List<DashboardDataCommentViewModel>>(recentComments)
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

        public ProfileSettingsViewModel RemoveProfilePhoto(string userId)
        {
            _dataProvider.RemoveProfilePhoto(userId);
            var user = _dataProvider.GetUserById(userId);
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

        public EditPostViewModel GetPostToEdit(int postId)
        {
            var post = _dataProvider.GetPostById(postId);
            return _mapper.Map<EditPostViewModel>(post);
        }

        public EditPostViewModel SavePost(EditPostViewModel post, string username)
        {
            if (post.Id == 0)
            {
                var slug = _slugService.CreateSlugFromPostTitle(post.Title);
                var blog = _dataProvider.GetBlog();
                // create new post
                var newPost = _dataProvider.CreatePost(post.Title, post.Content, post.Categories, slug, username, post.Published, blog.Id);
                return _mapper.Map<EditPostViewModel>(newPost);
            }
            else
            {
                // update existing post
                _dataProvider.UpdatePost(post.Id, post.Title, post.Content, post.Categories, post.Published);
                return post;
            }
        }

        public async Task<MediaViewModel> UploadMedia(IFormFile file)
        {
            var subFolder = DateTime.Now.ToString("yyyy/MM/dd");
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var bytes = ms.ToArray();
                var location = await _mediaStorageService.SaveMedia($"/blog-media/{subFolder}/", file.FileName, bytes)
                    .ConfigureAwait(false);

                return new MediaViewModel
                {
                    Location = location
                };
            }
        }
    }
}

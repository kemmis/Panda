using System.Collections.Generic;
using System.Linq;
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

        public PostService(IPandaPressDataProvider dataProvider, IMapper mapper)
        {
            this._dataProvider = dataProvider;
            this._mapper = mapper;
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
            return _dataProvider.CreatePost(request.Title, request.Content, request.Username, request.Publish, request.BlogId);
        }
    }
}

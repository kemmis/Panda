using System.Collections.Generic;
using AutoMapper;
using PandaPress.Core.Contracts;
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

        public void EditPost(string blogId, string postId, PostEditRequest request, bool publish)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PostEditRequest> GetCategories(string blogId, string userName)
        {
            throw new System.NotImplementedException();
        }

        public PostViewModel GetPost(string blogId, string postId)
        {
            throw new System.NotImplementedException();
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

        public void NewPost(string blogId, PostCreateRequest request, bool publish, string authorDisplayName)
        {
            throw new System.NotImplementedException();
        }
    }
}

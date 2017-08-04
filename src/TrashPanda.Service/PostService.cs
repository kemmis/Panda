using System.Collections.Generic;
using AutoMapper;
using PandaPress.Core.Contracts;
using PandaPress.Core.Models.Request;
using PandaPress.Core.Models.View;

namespace PandaPress.Service
{
    public class PostService : IPostService
    {
        private readonly IPandaPressDataProvider dataProvider;
        private readonly IMapper mapper;

        public PostService(IPandaPressDataProvider dataProvider, IMapper mapper)
        {
            this.dataProvider = dataProvider;
            this.mapper = mapper;
        }

        public PostViewModel GetPostBySlug(string slug)
        {
            var post = dataProvider.GetPostBySlug(slug);

            if (post == null) return null;
            
            return mapper.Map<PostViewModel>(post);
        }

        public PostListViewModel GetPostList(PostListRequest request)
        {
            var posts = dataProvider.GetPosts(request.PageSize, request.PageIndex);
            return new PostListViewModel
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Posts = mapper.Map<List<PostViewModel>>(posts.posts),
                TotalPosts = posts.totalPosts
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using cloudscribe.MetaWeblog;
using cloudscribe.MetaWeblog.Models;
using Microsoft.AspNetCore.Identity;
using PandaPress.Core.Contracts;
using PandaPress.Core.Models.Data;
using PandaPress.Core.Models.Request;

namespace PandaPress.Service
{
    public class MetaWeblogService : IMetaWeblogService
    {
        private readonly IPostService _postService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public MetaWeblogService(IPostService postService, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _postService = postService;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public Task<bool> DeletePage(string blogId, string pageId, string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePost(string blogId, string postId, string userName, string password)
        {
            _postService.DeletePost(blogId, postId);
            return Task.FromResult(true);
        }

        public Task<bool> EditPage(string blogId, string pageId, string userName, string password, PageStruct page, bool publish)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditPost(string blogId, string postId, string userName, string password, PostStruct post, bool publish)
        {
            _postService.EditPost(blogId, postId, new PostEditRequest
            {

            }, publish);
            return Task.FromResult(true);
        }

        public async Task<List<CategoryStruct>> GetCategories(string blogId, string userName, string password, CancellationToken cancellationToken)
        {
            if (!IsAuthenticated(userName, password).Result)
            {
                return null;
            }
            var categories = _postService.GetCategories();
            return _mapper.Map<List<CategoryStruct>>(categories);
        }

        public Task<PageStruct> GetPage(string blogId, string pageId, string userName, string password, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<PageStruct>> GetPageList(string blogId, string userName, string password, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<PageStruct>> GetPages(string blogId, string userName, string password, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PostStruct> GetPost(string blogId, string postId, string userName, string password, CancellationToken cancellationToken)
        {
            var post = _postService.GetPost(blogId, postId);
            var postStruct = new PostStruct
            {

            };
            return Task.FromResult(postStruct);
        }

        public Task<List<PostStruct>> GetRecentPosts(string blogId, string userName, string password, int numberOfPosts, CancellationToken cancellationToken)
        {
            var posts = _postService.GetPostList(new PostListRequest
            {
                PageSize = numberOfPosts,
                PageIndex = 0
            });
            // map to poststruct
            var metaPosts = new List<PostStruct>();
            return Task.FromResult(metaPosts);
        }

        public async Task<List<BlogInfoStruct>> GetUserBlogs(string key, string userName, string password, CancellationToken cancellationToken)
        {
            if (!IsAuthenticated(userName, password).Result)
            {
                return null;
            }

            var blogs = _postService.GetBlogsForUser(userName);
            return _mapper.Map<List<BlogInfoStruct>>(blogs);
        }

        public Task<string> NewCategory(string blogId, string category, string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<MediaInfoStruct> NewMediaObject(string blogId, string userName, string password, MediaObjectStruct mediaObject)
        {
            throw new NotImplementedException();
        }

        public Task<string> NewPage(string blogId, string userName, string password, PageStruct newPage, bool publish)
        {
            throw new NotImplementedException();
        }

        public Task<string> NewPost(string blogId, string userName, string password, PostStruct newPost, bool publish, string authorDisplayName)
        {
            _postService.NewPost(blogId, new PostCreateRequest
            {

            }, publish, authorDisplayName);
            return Task.FromResult("");
        }

        private async Task<bool> IsAuthenticated(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, true, false).ConfigureAwait(false);
            return result.Succeeded;
        }
    }
}

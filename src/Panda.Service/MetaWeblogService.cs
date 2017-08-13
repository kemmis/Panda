using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using cloudscribe.MetaWeblog;
using cloudscribe.MetaWeblog.Models;
using Microsoft.AspNetCore.Identity;
using Panda.Core.Contracts;
using Panda.Core.Models.Data;
using Panda.Core.Models.Request;

namespace Panda.Service
{
    public class MetaWeblogService : IMetaWeblogService
    {
        private readonly IBlogService _blogService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public MetaWeblogService(IBlogService blogService, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _blogService = blogService;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public Task<bool> DeletePage(string blogId, string pageId, string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePost(string blogId, string postId, string userName, string password)
        {
            _blogService.DeletePost(blogId, postId);
            return Task.FromResult(true);
        }

        public Task<bool> EditPage(string blogId, string pageId, string userName, string password, PageStruct page, bool publish)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditPost(string blogId, string postId, string userName, string password, PostStruct post, bool publish)
        {
            await Authenticate(userName, password);

            var request = _mapper.Map<PostEditRequest>(post);
            request.PostId = int.Parse(postId);
            request.Username = userName;
            request.Publish = publish;

            _blogService.EditPost(request);

            return true;
        }

        public async Task<List<CategoryStruct>> GetCategories(string blogId, string userName, string password, CancellationToken cancellationToken)
        {
            await Authenticate(userName, password);
            var categories = _blogService.GetCategories();
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

        public async Task<PostStruct> GetPost(string blogId, string postId, string userName, string password, CancellationToken cancellationToken)
        {
            await Authenticate(userName, password);

            var post = _blogService.GetPost(postId);

            return _mapper.Map<PostStruct>(post);
        }

        public Task<List<PostStruct>> GetRecentPosts(string blogId, string userName, string password, int numberOfPosts, CancellationToken cancellationToken)
        {
            var posts = _blogService.GetPostList(new PostListRequest
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
            await Authenticate(userName, password);

            var blogs = _blogService.GetBlogsForUser(userName);
            return _mapper.Map<List<BlogInfoStruct>>(blogs);
        }

        public Task<string> NewCategory(string blogId, string category, string userName, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<MediaInfoStruct> NewMediaObject(string blogId, string userName, string password, MediaObjectStruct mediaObject)
        {
            var url = await _blogService.SaveMedia(mediaObject.bytes, mediaObject.name).ConfigureAwait(false);
            var result = new MediaInfoStruct { url = url };
            return result;
        }

        public Task<string> NewPage(string blogId, string userName, string password, PageStruct newPage, bool publish)
        {
            throw new NotImplementedException();
        }

        public async Task<string> NewPost(string blogId, string userName, string password, PostStruct newPost, bool publish, string authorDisplayName)
        {
            await Authenticate(userName, password);

            var request = _mapper.Map<PostCreateRequest>(newPost);
            request.Username = userName;
            request.BlogId = int.Parse(blogId);
            request.Publish = publish;

            var post = _blogService.NewPost(request);

            return post.Id.ToString();
        }

        private async Task Authenticate(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, true, false).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                throw new SecurityException();
            }
        }
    }
}

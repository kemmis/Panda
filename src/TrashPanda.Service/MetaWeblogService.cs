using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using cloudscribe.MetaWeblog;
using cloudscribe.MetaWeblog.Models;
using PandaPress.Core.Contracts;

namespace PandaPress.Service
{
    public class MetaWeblogService : IMetaWeblogService
    {
        private readonly IPostService _postService;

        public MetaWeblogService(IPostService postService)
        {
            _postService = postService;
        }

        public Task<bool> DeletePage(string blogId, string pageId, string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePost(string blogId, string postId, string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditPage(string blogId, string pageId, string userName, string password, PageStruct page, bool publish)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditPost(string blogId, string postId, string userName, string password, PostStruct post, bool publish)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryStruct>> GetCategories(string blogId, string userName, string password, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Task<List<PostStruct>> GetRecentPosts(string blogId, string userName, string password, int numberOfPosts, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlogInfoStruct>> GetUserBlogs(string key, string userName, string password, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}

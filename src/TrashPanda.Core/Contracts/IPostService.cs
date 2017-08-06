using System.Collections.Generic;
using PandaPress.Core.Models.Request;
using PandaPress.Core.Models.View;

namespace PandaPress.Core.Contracts
{
    public interface IPostService
    {
        PostViewModel GetPostBySlug(string slug);
        PostListViewModel GetPostList(PostListRequest request);

        void DeletePost(string blogId, string postId);
        void EditPost(string blogId, string postId, PostEditRequest request, bool publish);
        IEnumerable<PostEditRequest> GetCategories(string blogId, string userName);
        PostViewModel GetPost(string blogId, string postId);
        IEnumerable<PostViewModel> GetRecentPosts(string blogId);
        void NewCategory(string blogId, string category);
        void NewMediaObject(string blogId);
        void NewPost(string blogId, PostCreateRequest request, bool publish, string authorDisplayName);
    }
}

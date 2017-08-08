using System.Collections.Generic;
using System.Threading.Tasks;
using PandaPress.Core.Models.Data;

namespace PandaPress.Core.Contracts
{
    public interface IPandaPressDataProvider
    {
        Task Init();
        Post GetPostBySlug(string slug);
        (IEnumerable<Post> posts, int totalPosts) GetPosts(int pageSize, int pageIndex);
        IEnumerable<Blog> GetBlogsForUser(string username);
        IEnumerable<Category> GetCategories();
        Post CreatePost(string title, string content, string slug, string username, bool publish, int blogId);
        Post GetPostById(int postId);
        void UpdatePost(int postId, string title, string content, bool publish);
        Blog GetBlog();
        Blog UpdateBlog(int blogId, string blogName);
        int GetNumPublishedPosts();
        int GetNumDrafts();
    }
}

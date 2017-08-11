using System.Collections.Generic;
using System.Threading.Tasks;
using PandaPress.Core.Models.Data;

namespace PandaPress.Core.Contracts
{
    public interface IPandaPressDataProvider
    {
        Task Init();
        Post GetPostBySlug(string slug);
        Category GetCategoryBySlug(string slug);
        (IEnumerable<Post> posts, int totalPosts) GetPosts(int pageSize, int pageIndex);
        IEnumerable<Blog> GetBlogsForUser(string username);
        IEnumerable<Category> GetCategories();
        Post CreatePost(string title, string content, List<string> categories, string slug, string username, bool publish, int blogId);
        Post GetPostById(int postId);
        void UpdatePost(int postId, string title, string content, List<string> categories, bool publish);
        Blog GetBlog();
        Blog UpdateBlog(int blogId, string blogName, string description, int postsPerPage);
        int GetNumPublishedPosts();
        int GetNumDrafts();
        IEnumerable<Post> GetPosts();
        IEnumerable<Category> GetCategoriesWithPostCategories();
        Category AddCategory(string title, string description, string slug);
        void DeleteCategory(int categoryId);
        ApplicationUser GetUserById(string userId);
        ApplicationUser UpdateUser(string userId, string displayName);
        Comment CreateComment(int postId, string authorName, string authorEmail, string text);
        void DeletePost(int postId);
        void UnDeletePost(int postId);
    }
}

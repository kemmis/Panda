using System.Collections.Generic;
using System.Threading.Tasks;
using PandaPress.Core.Models.Data;
using PandaPress.Core.Models.Request;
using PandaPress.Core.Models.View;

namespace PandaPress.Core.Contracts
{
    public interface IPostService
    {
        PostViewModel GetPostBySlug(string slug);
        PostListViewModel GetPostList(PostListRequest request);

        void DeletePost(string blogId, string postId);
        void EditPost(PostEditRequest request);
        IEnumerable<PostEditRequest> GetCategories(string blogId, string userName);
        PostViewModel GetPost(string postId);
        IEnumerable<PostViewModel> GetRecentPosts(string blogId);
        void NewCategory(string blogId, string category);
        void NewMediaObject(string blogId);
        Post NewPost(PostCreateRequest request);
        List<Blog> GetBlogsForUser(string username);
        List<Category> GetCategories();
        Task<string> SaveMedia(byte[] bytes, string name);
        SettingsViewModel GetBlogSettings();
        SettingsViewModel SaveBlogSettings(SettingsViewModel settings);
        DashboardDataViewModel GetDashboardData();
        ContentViewModel GetContent();
        CategoryContentViewModel AddCategory(string title, string description);
        void DeleteCategory(int categoryId);
        ProfileSettingsViewModel GetProfileSettings(string userId);
        ProfileSettingsViewModel UpdateProfileSettings(string userId, ProfileSettingsUpdateRequest request);

    }
}

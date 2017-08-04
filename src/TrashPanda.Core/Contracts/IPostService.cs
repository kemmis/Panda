using PandaPress.Core.Models.Request;
using PandaPress.Core.Models.View;

namespace PandaPress.Core.Contracts
{
    public interface IPostService
    {
        PostViewModel GetPostBySlug(string slug);
        PostListViewModel GetPostList(PostListRequest request);
    }
}

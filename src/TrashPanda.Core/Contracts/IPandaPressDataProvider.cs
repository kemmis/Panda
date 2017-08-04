using System.Collections.Generic;
using PandaPress.Core.Models.Data;

namespace PandaPress.Core.Contracts
{
    public interface IPandaPressDataProvider
    {
        void Init();
        Post GetPostBySlug(string slug);
        (IEnumerable<Post> posts, int totalPosts) GetPosts(int pageSize, int pageIndex);
    }
}

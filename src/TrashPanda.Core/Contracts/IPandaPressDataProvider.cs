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
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TrashPanda.Core.Models.Data;

namespace TrashPanda.Core.Contracts
{
    public interface IPandaPressDataProvider
    {
        void Init();
        Post GetPostBySlug(string slug);
        (IEnumerable<Post> posts, int totalPosts) GetPosts(int pageSize, int pageIndex);
    }
}

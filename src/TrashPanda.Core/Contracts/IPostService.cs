using System;
using System.Collections.Generic;
using System.Text;
using PandaPress.Core.Models.Request;
using PandaPress.Core.Models.View;
using TrashPanda.Core.Models.View;

namespace TrashPanda.Core.Contracts
{
    public interface IPostService
    {
        PostViewModel GetPostBySlug(string slug);
        PostListViewModel GetPostList(PostListRequest request);
    }
}

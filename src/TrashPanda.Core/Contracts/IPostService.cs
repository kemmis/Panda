using System;
using System.Collections.Generic;
using System.Text;
using TrashPanda.Core.Models.View;

namespace TrashPanda.Core.Contracts
{
    public interface IPostService
    {
        PostViewModel GetPostBySlug(string slug);
    }
}

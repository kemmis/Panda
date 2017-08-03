using System;
using System.Collections.Generic;
using System.Text;
using TrashPanda.Core.Models.Data;

namespace TrashPanda.Core.Contracts
{
    public interface ITrashPandaDataProvider
    {
        Post GetPostBySlug(string slug);
    }
}

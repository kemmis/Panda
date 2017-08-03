using System;
using System.Collections.Generic;
using System.Text;
using TrashPanda.Core.Models.Data;

namespace TrashPanda.Core.Contracts
{
    public interface ITrashPandaDataProvider
    {
        void Init();
        Post GetPostBySlug(string slug);
    }
}

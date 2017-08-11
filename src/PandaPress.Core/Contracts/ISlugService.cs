using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Contracts
{
    public interface ISlugService
    {
        string CreateSlugFromPostTitle(string title);
        string CreateSlugFromCategoryTitle(string title);
    }
}

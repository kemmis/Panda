using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Contracts
{
    public interface ISlugService
    {
        string CreateSlugFromTitle(string title);
    }
}

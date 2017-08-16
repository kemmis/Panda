using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Core.Contracts
{
    public interface IGravatarService
    {
        string GetGravatarHash(string email);
    }
}

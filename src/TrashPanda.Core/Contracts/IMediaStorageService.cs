using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PandaPress.Core.Contracts
{
    public interface IMediaStorageService
    {
        Task<string> SaveMedia(string mediaVirtualPath, string fileName, byte[] bytes);
    }
}

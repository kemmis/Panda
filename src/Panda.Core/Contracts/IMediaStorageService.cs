using System.Threading.Tasks;

namespace Panda.Core.Contracts
{
    public interface IMediaStorageService
    {
        Task<string> SaveMedia(string mediaVirtualPath, string fileName, byte[] bytes);
    }
}

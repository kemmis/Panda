using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Panda.Core.Contracts;

namespace Panda.Service
{
    public class FileSystemMediaStorageService : IMediaStorageService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        
        public FileSystemMediaStorageService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        private string RootPath => _hostingEnvironment.WebRootPath;

        public async Task<string> SaveMedia(string mediaVirtualPath, string fileName, byte[] bytes)
        {
            var newUrl = mediaVirtualPath + fileName;

            var fullMediaPath = newUrl.Substring(0, newUrl.LastIndexOf('/'));

            await EnsureFsPath(fullMediaPath);
            
            var fsPath = RootPath + newUrl.Replace('/', Path.DirectorySeparatorChar);

            await File.WriteAllBytesAsync(fsPath, bytes);

            return newUrl;
        }

        private async Task EnsureFsPath(string mediaVirtualPath)
        {
            var fsPath = RootPath + mediaVirtualPath.Replace('/', Path.DirectorySeparatorChar);
            if (Directory.Exists(fsPath)) return; //nothing to do

            var segments = mediaVirtualPath.Split('/');

            EnsureFolderPaths(RootPath, segments);
        }


        protected void EnsureFolderPaths(string existingPath, string[] folderNameSegments)
        {
            if (string.IsNullOrEmpty(existingPath)) return;
            if (folderNameSegments.Length == 0) return;
            if (!Directory.Exists(existingPath)) return;

            var partial = existingPath;
            for (var i = 0; i < folderNameSegments.Length; i++)
            {
                partial = Path.Combine(partial, folderNameSegments[i]);

                if (!Directory.Exists(partial))
                {
                    try
                    {
                        Directory.CreateDirectory(partial);
                    }
                    catch
                    {
                        //Log.LogError($"failed to create folder {partial}", ex);
                    }
                }
            }
        }
    }
}

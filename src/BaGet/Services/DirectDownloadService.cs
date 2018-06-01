using System.Threading.Tasks;
using BaGet.Core.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Core;
using NuGet.Versioning;

namespace BaGet.Services
{
    public class DirectDownloadService : IDownloadService
    {
        private readonly IPackageStorageService _storage;

        public async Task<IActionResult> DownloadPackageAsync(string id, NuGetVersion version)
        {
            var identity = new PackageIdentity(id, version);
            var packageStream = await _storage.GetPackageStreamAsync(identity);

            return new FileStreamResult(packageStream, "application/octet-stream");
        }

        public async Task<IActionResult> DownloadNuspecAsync(string id, NuGetVersion version)
        {
            var identity = new PackageIdentity(id, version);

            return new FileStreamResult(await _storage.GetNuspecStreamAsync(identity), "text/xml");
        }

        public async Task<IActionResult> DownloadReadmeAsync(string id, NuGetVersion version)
        {
            var identity = new PackageIdentity(id, version);

            return new FileStreamResult(await _storage.GetReadmeStreamAsync(identity), "text/markdown");
        }
    }
}

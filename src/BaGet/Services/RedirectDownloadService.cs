using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;

namespace BaGet.Services
{
    public class RedirectDownloadService : IDownloadService
    {
        public Task<IActionResult> DownloadNuspecAsync(string id, NuGetVersion version)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DownloadPackageAsync(string id, NuGetVersion version)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DownloadReadmeAsync(string id, NuGetVersion version)
        {
            throw new NotImplementedException();
        }
    }
}

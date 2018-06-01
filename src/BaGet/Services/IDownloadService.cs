using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;

namespace BaGet.Services
{
    public interface IDownloadService
    {
        Task<IActionResult> DownloadPackageAsync(string id, NuGetVersion version);

        Task<IActionResult> DownloadNuspecAsync(string id, NuGetVersion version);

        Task<IActionResult> DownloadReadmeAsync(string id, NuGetVersion version);
    }
}

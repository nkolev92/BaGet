using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaGet.Tools.ImportDownloads
{
    public interface IPackageDownloadsSource
    {
        Task<Dictionary<string, Dictionary<string, long>>> GetPackageDownloadsAsync();
    }
}

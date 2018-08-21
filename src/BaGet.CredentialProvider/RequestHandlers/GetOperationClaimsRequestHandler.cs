using System.Threading.Tasks;
using BaGet.CredentialProvider.Logging;
using NuGet.Protocol.Plugins;

namespace BaGet.CredentialProvider.RequestHandlers
{
    internal class GetOperationClaimsRequestHandler : RequestHandlerBase<GetOperationClaimsRequest, GetOperationClaimsResponse>
    {
        public GetOperationClaimsRequestHandler(ILogger logger) : base(logger)
        {
        }

        public override Task<GetOperationClaimsResponse> HandleRequestAsync(GetOperationClaimsRequest request)
        {
            if (request.PackageSourceRepository == null && request.ServiceIndex == null)
            {
                return Task.FromResult(new GetOperationClaimsResponse(new OperationClaim[] { OperationClaim.Authentication }));
            }

            return Task.FromResult(new GetOperationClaimsResponse(new OperationClaim[] { }));

        }
    }
}

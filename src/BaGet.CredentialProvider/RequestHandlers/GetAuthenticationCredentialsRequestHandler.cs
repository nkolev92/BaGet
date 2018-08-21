using System;
using System.Threading.Tasks;
using BaGet.CredentialProvider.Logging;
using NuGet.Protocol.Plugins;

namespace BaGet.CredentialProvider.RequestHandlers
{
    class GetAuthenticationCredentialsRequestHandler : RequestHandlerBase<GetAuthenticationCredentialsRequest, GetAuthenticationCredentialsResponse>
    {
        public GetAuthenticationCredentialsRequestHandler(ILogger logger) : base(logger)
        {
        }

        public override Task<GetAuthenticationCredentialsResponse> HandleRequestAsync(GetAuthenticationCredentialsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

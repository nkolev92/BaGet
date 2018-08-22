using System;
using System.Threading.Tasks;
using BaGet.CredentialProvider.Logging;
using NuGet.Protocol.Plugins;

namespace BaGet.CredentialProvider.RequestHandlers
{
    class SetCredentialsRequestHandler : RequestHandlerBase<SetCredentialsRequest, SetCredentialsResponse>
    {
        public SetCredentialsRequestHandler(ILogger logger) : base(logger)
        {
        }

        public override Task<SetCredentialsResponse> HandleRequestAsync(SetCredentialsRequest request)
        {
            // TODO NK - Set the proxy settings. This credential request is only valid if it's setting a proxy.
            return Task.FromResult(new SetCredentialsResponse(MessageResponseCode.Success));
        }
    }
}

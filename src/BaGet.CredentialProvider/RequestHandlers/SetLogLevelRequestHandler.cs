using System;
using System.Threading.Tasks;
using BaGet.CredentialProvider.Logging;
using NuGet.Protocol.Plugins;

namespace BaGet.CredentialProvider.RequestHandlers
{
    class SetLogLevelRequestHandler : RequestHandlerBase<SetLogLevelRequest, SetLogLevelResponse>
    {
        public SetLogLevelRequestHandler(ILogger logger) : base(logger)
        {
        }

        public override Task<SetLogLevelResponse> HandleRequestAsync(SetLogLevelRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

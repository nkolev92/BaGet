using NuGet.Protocol.Plugins;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BaGet.CredentialProvider.RequestHandlers
{
    internal class InitializeRequestHandler : RequestHandlerBase<InitializeRequest, InitializeResponse>
    {
        public InitializeRequestHandler(TraceSource logger) : base(logger)
        {
        }

        public override Task<InitializeResponse> HandleRequestAsync(InitializeRequest request)
        {
            return Task.FromResult(new InitializeResponse(MessageResponseCode.Success));
        }
    }
}

﻿using System.Globalization;
using System.Threading.Tasks;
using BaGet.CredentialProvider.Logging;
using NuGet.Protocol.Plugins;

namespace BaGet.CredentialProvider.RequestHandlers
{
    internal class InitializeRequestHandler : RequestHandlerBase<InitializeRequest, InitializeResponse>
    {
        public InitializeRequestHandler(ILogger logger) : base(logger)
        {
        }

        public override Task<InitializeResponse> HandleRequestAsync(InitializeRequest request)
        {
            CultureInfo.CurrentCulture = new CultureInfo(request.Culture);
            return Task.FromResult(new InitializeResponse(MessageResponseCode.Success));
        }
    }
}
﻿using System;
using System.Threading;
using System.Threading.Tasks;
using BaGet.CredentialProvider.Logging;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;

namespace BaGet.CredentialProvider.RequestHandlers
{
    internal abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler
            where TResponse : class
    {
        public virtual CancellationToken CancellationToken { get; private set; } = CancellationToken.None;

        public IConnection Connection { get; private set; }

        public ILogger Logger { get; }

        protected RequestHandlerBase(ILogger logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleResponseAsync(IConnection connection, Message message, IResponseHandler responseHandler, CancellationToken cancellationToken)
        {
            Connection = connection;

            CancellationToken = cancellationToken;

            TRequest request = MessageUtilities.DeserializePayload<TRequest>(message);

            Logger.Log(NuGet.Common.LogLevel.Verbose, $"Handling request '{message.Method}' - {message.Payload.ToString(Formatting.None)}'");

            TResponse response = await HandleRequestAsync(request).ConfigureAwait(continueOnCapturedContext: false);

            Logger.Log(NuGet.Common.LogLevel.Verbose, $"Sending response: '{JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.None })}'");

            await responseHandler.SendResponseAsync(message, response, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
        }

        public abstract Task<TResponse> HandleRequestAsync(TRequest request);
    }
}

using BaGet.CredentialProvider.Logging;
using BaGet.CredentialProvider.RequestHandlers;
using NuGet.Common;
using NuGet.Protocol.Plugins;
using System;
using System.Threading;
using System.Threading.Tasks;
using ILogger = BaGet.CredentialProvider.Logging.ILogger;

namespace BaGet.CredentialProvider
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                tokenSource.Cancel();
                eventArgs.Cancel = true;
            };
            // TODO NK - parse the arguments, this should only work when the -plugin argument is passed.
            var logger = new MultiLogger();
            logger.Add(new FileLogger());

            var requestHandlers = new RequestHandlerCollection
                {
                    { MessageMethod.GetAuthenticationCredentials, new GetAuthenticationCredentialsRequestHandler(logger) },
                    { MessageMethod.GetOperationClaims, new GetOperationClaimsRequestHandler(logger) },
                    { MessageMethod.Initialize, new InitializeRequestHandler(logger) },
                    { MessageMethod.SetLogLevel, new SetLogLevelRequestHandler(logger) },
                    { MessageMethod.SetCredentials, new SetCredentialsRequestHandler(logger) },
                };
            logger.Log(LogLevel.Debug, "plugin mode");

            using (IPlugin plugin = await PluginFactory.CreateFromCurrentProcessAsync(requestHandlers, PluginUtilities.GetConnectionOptions(), tokenSource.Token).ConfigureAwait(continueOnCapturedContext: false))
            {
                if (plugin.Connection.ProtocolVersion != ProtocolConstants.CurrentVersion)
                {
                    // TODO NK - Terminate the plugin here. need to make sure that the plugin is the correct version.
                    throw new NotSupportedException();
                }

                logger.Add(new PluginConnectionLogger(plugin.Connection));
                await RunNuGetPluginsAsync(plugin, logger, TimeSpan.FromMinutes(2), tokenSource.Token).ConfigureAwait(continueOnCapturedContext: false);
            }

            return 0;
        }

        internal static async Task RunNuGetPluginsAsync(IPlugin plugin, ILogger logger, TimeSpan timeout, CancellationToken cancellationToken)
        {
            SemaphoreSlim semaphore = new SemaphoreSlim(0);

            plugin.Connection.Faulted += (sender, a) =>
            {
                logger.Log(LogLevel.Error, string.Format("The plugin faulted", $"{a.Message?.Type} {a.Message?.Method} {a.Message?.RequestId}"));
                logger.Log(LogLevel.Error, a.Exception.ToString());
            };

            plugin.Closed += (sender, a) => semaphore.Release();

            bool success = await semaphore.WaitAsync(timeout, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);

            if (!success)
            {
                logger.Log(LogLevel.Error, "The plugin timed out.");
            }
        }

    }
}



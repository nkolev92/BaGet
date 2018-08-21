using NuGet.Common;
using NuGet.Protocol.Plugins;
using NuGet.Versioning;

namespace BaGet.CredentialProvider
{
    public static class PluginUtilities
    {
        private const string _handshakeTimeoutEnvironmentVariable = "NUGET_PLUGIN_HANDSHAKE_TIMEOUT_IN_SECONDS";
        private const string _requestTimeoutEnvironmentVariable = "NUGET_PLUGIN_REQUEST_TIMEOUT_IN_SECONDS";
        // TODO NK - both can technically set the timeout.  Is that done during the initialization or?

        internal static SemanticVersion PluginVersion = new SemanticVersion(2, 0, 0);

        public static ConnectionOptions GetConnectionOptions()
        {
            var reader = new EnvironmentVariableWrapper();

            var handshakeTimeoutInSeconds = reader.GetEnvironmentVariable(_handshakeTimeoutEnvironmentVariable);
            var requestTimeoutInSeconds = reader.GetEnvironmentVariable(_requestTimeoutEnvironmentVariable);

            var handshakeTimeout = TimeoutUtilities.GetTimeout(handshakeTimeoutInSeconds, ProtocolConstants.HandshakeTimeout);
            var requestTimeout = TimeoutUtilities.GetTimeout(requestTimeoutInSeconds, ProtocolConstants.RequestTimeout);

            return new ConnectionOptions(
                   protocolVersion: PluginVersion,
                   minimumProtocolVersion: PluginVersion,
                   handshakeTimeout: handshakeTimeout,
                   requestTimeout: requestTimeout);
        }
    }
}

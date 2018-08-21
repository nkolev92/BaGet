using NuGet.Common;

namespace BaGet.CredentialProvider.Logging
{
    internal abstract class ILogger
    {
        public void LogError(string message)
        {
            Log(LogLevel.Error, message);
        }

        public void LogDebug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        public void LogInformation(string message)
        {
            Log(LogLevel.Information, message);
        }

        public void LogMinimal(string message)
        {
            Log(LogLevel.Minimal, message);
        }

        public void LogVerbose(string message)
        {
            Log(LogLevel.Verbose, message);
        }

        public void LogWarning(string message)
        {
            Log(LogLevel.Warning, message);
        }

        public abstract void Log(LogLevel level, string message);

        public abstract void SetLogLevel(LogLevel newLogLevel);
    }
}

using System.Diagnostics;

namespace BaGet.CredentialProvider
{
    internal static class TraceLoggerExtensionMethods
    {
        public static void Error(this TraceSource traceSource, string message)
        {
            traceSource.TraceEvent(TraceEventType.Error, 0, message);
        }

        public static void Verbose(this TraceSource traceSource, string message)
        {
            traceSource.TraceEvent(TraceEventType.Verbose, 0, message);
        }
    }
}

using System.Collections.Generic;
using NuGet.Common;

namespace BaGet.CredentialProvider.Logging
{
    internal class MultiLogger : ILogger
    {
        private IList<ILogger> loggers = new List<ILogger>();

        public override void Log(LogLevel level, string message)
        {
            foreach (var logger in loggers)
            {
                logger.Log(level, message);
            }
        }

        public override void SetLogLevel(LogLevel newLogLevel)
        {
            foreach (var logger in loggers)
            {
                logger.SetLogLevel(newLogLevel);
            }
        }

        public void Add(ILogger logger)
        {
            loggers.Add(logger);
        }
    }
}

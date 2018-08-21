using System.Threading;
using NuGet.Common;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Concurrent;

namespace BaGet.CredentialProvider.Logging
{
    internal class PluginConnectionLogger : ILogger
    {
        private readonly IConnection connection;
        private LogLevel minLogLevel = LogLevel.Debug;
        private bool allowLogWrites = false;
        private ConcurrentQueue<Tuple<LogLevel, string, DateTime>> bufferedLogs = new ConcurrentQueue<Tuple<LogLevel, string, DateTime>>();

        internal PluginConnectionLogger(IConnection connection)
        {
            this.connection = connection;
        }

        public void SetLogLevel(LogLevel newLogLevel)
        {
            minLogLevel = newLogLevel;
            allowLogWrites = true;
        }

        public void Log(LogLevel level, string message)
        {
            if (!allowLogWrites)
            {
                // cheap reserve, if it swaps out after we add, meh, we miss one log
                var buffer = bufferedLogs;
                if (buffer != null)
                {
                    buffer.Enqueue(Tuple.Create(level, message, DateTime.Now));
                }
            }
            else
            {
                if (bufferedLogs != null)
                {
                    ConcurrentQueue<Tuple<LogLevel, string, DateTime>> buffer = null;
                    buffer = Interlocked.CompareExchange(ref bufferedLogs, null, bufferedLogs);

                    if (buffer != null)
                    {
                        foreach (var log in buffer)
                        {
                            if (log.Item1 >= minLogLevel)
                            {
                                WriteLog(log.Item1, GetLogPrefix(log.Item3) + log.Item2);
                            }
                        }
                    }
                }

                if (level >= minLogLevel)
                {
                    WriteLog(level, GetLogPrefix(null) + message);
                }
            }
        }

        private void WriteLog(LogLevel logLevel, string message)
        {
            connection.SendRequestAndReceiveResponseAsync<LogRequest, LogResponse>(
                MessageMethod.Log,
                new LogRequest(logLevel, message),
                CancellationToken.None);
        }

        private string GetLogPrefix(DateTime? bufferedLogTime)
        {
            string timeString = bufferedLogTime.HasValue ? bufferedLogTime.Value.ToString(".HHmmss") : null;
            return $"[BaGet CredentialProvider {timeString}]";
        }
    }
}
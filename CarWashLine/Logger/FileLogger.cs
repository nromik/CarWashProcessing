using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CarWashLine.Logger
{
    public class FileLogger : ILogger
    {
        private StringBuilder sb;
        private string filePath;
        private object _lock = new object();
        private bool _isWriteLog = false;
        public FileLogger(string path)
        {
            filePath = path;
            sb = new StringBuilder();
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {

            if (formatter != null)
            {
                lock (sb)
                {
                    sb.AppendLine(DateTime.Now + " "+ formatter(state, exception));
                }
            }

            Task.Run( async () => await WriteLog());
        }

        private async Task WriteLog()
        {
            if (_isWriteLog || sb.Length == 0) return;

            string logContext = null;
            lock (sb)
            {
                _isWriteLog = true;
                logContext = sb.ToString();
                sb.Clear();
            }

            lock (_lock)
            {
                File.AppendAllText(filePath, logContext);
            }

            await Task.Delay(5000);

            lock (sb)
            {
                _isWriteLog = false;
            }

            GC.Collect();
        }

    }
}
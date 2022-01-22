using System;
using System.Diagnostics;

namespace Codelux.Logging
{
    public class DebugLogger : ILogger
    {
        private readonly bool _debugAvailable;

        public DebugLogger()
        {
            // This will not be evaluated if we're not in debug mode
            Debug.Assert(_debugAvailable = true);
        }

        public void LogEvent(LogType logType, string message)
        {
            string logMessage = $"[{logType:F}] {message}";
            LogMessage(logMessage);
        }

        public void LogEvent<T>(LogType logType, string message)
        {
            string logMessage = $"[{logType:F}] ({typeof(T).Name}) {message}";
            LogMessage(logMessage);
        }

        public void LogEvent(LogType logType, string message, int level)
        {
            string logMessage = $"[{logType:F}] Level: {level} - {message}";
            LogMessage(logMessage);
        }

        public void LogEvent<T>(LogType logType, string message, int level)
        {
            string logMessage = $"[{logType:F}] ({typeof(T).Name}) Level: {level} - {message}";
            LogMessage(logMessage);
        }

        private void LogMessage(string message)
        {
            if (_debugAvailable)
                Debug.WriteLine(message);
            else
                Console.WriteLine(message);
        }
    }
}

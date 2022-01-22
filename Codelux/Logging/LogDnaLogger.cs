using System;
using RedBear.LogDNA;

namespace Codelux.Logging
{
    public class LogDnaLogger : ILogger
    {
        private readonly IApiClient _apiClient;

        public LogDnaLogger(string ingestionKey, string serviceName)
        {
            if (string.IsNullOrEmpty(ingestionKey)) throw new ArgumentNullException(nameof(ingestionKey));
            if (string.IsNullOrEmpty(serviceName)) throw new ArgumentNullException(nameof(serviceName));

            var configManager = new ConfigurationManager(ingestionKey)
            {
                Tags = new [] { serviceName }
            };

            _apiClient = configManager.Initialise();
        }


        public void LogEvent(LogType logType, string message)
        {
            _apiClient.Connect();
            _apiClient.AddLine(new("Log Type", $"[{logType:F}] {message}"));
            _apiClient.Send(message);
            _apiClient.Disconnect();
        }

        public void LogEvent<T>(LogType logType, string message)
        {
            _apiClient.Connect();
            _apiClient.AddLine(new("Log Type", $"[{logType:F}] ({typeof(T).Name}) {message}"));
            _apiClient.Send(message);
            _apiClient.Disconnect();
        }

        public void LogEvent(LogType logType, string message, int level)
        {
            _apiClient.Connect();
            _apiClient.AddLine(new("Log Type", $"[{logType:F}] Level: {level} - {message}"));
            _apiClient.Send(message);
            _apiClient.Disconnect();
        }

        public void LogEvent<T>(LogType logType, string message, int level)
        {
            _apiClient.Connect();
            _apiClient.AddLine(new("Log Type", $"[{logType:F}] ({typeof(T).Name}) Level: {level} - {message}"));
            _apiClient.Send(message);
            _apiClient.Disconnect();
        }
    }
}

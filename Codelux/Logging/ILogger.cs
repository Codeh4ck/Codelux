namespace Codelux.Logging
{
    public interface ILogger
    {
        void LogEvent(LogType logType, string message);
        void LogEvent<T>(LogType logType, string message);
        void LogEvent(LogType logType, string message, int level);
        void LogEvent<T>(LogType logType, string message, int level);
    }
}

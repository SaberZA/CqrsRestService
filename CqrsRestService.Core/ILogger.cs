using CqrsRestService.Core;

namespace CqrsRestService.CorePortable
{
    public interface ILogger
    {
        void Log(string message, LogLevel logLevel = LogLevel.Information);
    }
}
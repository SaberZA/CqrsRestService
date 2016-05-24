using System;
using CqrsRestService.Core;

namespace CqrsRestService.CorePortable
{
    public class NullLogger : ILogger
    {
        public void Log(string message, LogLevel logLevel = LogLevel.Information)
        {
            
        }
    }
}
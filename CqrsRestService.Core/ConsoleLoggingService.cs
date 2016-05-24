using CqrsRestService.CorePortable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsRestService.Core
{
    public class ConsoleLoggingService : ILogger//, IServerLogger
    {
        private string _hostInfo;

        public ConsoleLoggingService()
        {
        }

        public ConsoleLoggingService(string hostInfo)
        {
            _hostInfo = hostInfo;
        }

        public void Log(string message, LogLevel logLevel = LogLevel.Information)
        {
            //var previousColor = Console.ForegroundColor;
            //var logColor = Console.ForegroundColor;

            //switch (logLevel)
            //{
            //    case LogLevel.Information: logColor = ConsoleColor.White; break;
            //    case LogLevel.Warning: logColor = ConsoleColor.Yellow; break;
            //    case LogLevel.Error: logColor = ConsoleColor.Red; break;
            //    default: logColor = ConsoleColor.White; break;
            //}

            //Console.ForegroundColor = logColor;
            Debug.WriteLine(message);
            //Console.ForegroundColor = previousColor;
        }




    }

    public enum LogLevel
    {
        Information,
        Warning,
        Error
    }
}

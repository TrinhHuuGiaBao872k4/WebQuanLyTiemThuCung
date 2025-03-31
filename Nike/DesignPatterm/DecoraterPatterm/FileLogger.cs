using System;
using System.Web;

namespace Nike.DesignPatterm.DecoraterPatterm
{
    public class FileLogger : ILogger
    {
        private static readonly object _lock = new object();

        public void Log(string message)
        {
            lock (_lock)
            {
                var logPath = HttpContext.Current.Server.MapPath("~/App_Data/log.txt");
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n");
            }
        }
    }
}
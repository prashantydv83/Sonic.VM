using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.IO;
using Sonic.VM.Contracts;


namespace Sonic.VM.Logging
{
    public class Logger : ILogger
    {
        private ILog log4NetLogger;
        private static ILogger myLogger;
        private static readonly object lockObject = new object();

        private Logger()
        {
            //var logConfig = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["LoggerConfig"]);
            var logConfig = new FileInfo(@"C:\\Projects\\Code\Sonic.VM\\Sonic.VM.Logging\\Logger.config");
            log4net.Config.XmlConfigurator.Configure(configFile: logConfig);
            log4NetLogger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static ILogger MyLogger
        {
            get
            {
                if (myLogger == null)
                {
                    lock (lockObject)
                    {
                        if (myLogger == null)
                            myLogger = new Logger();
                    }
                }

                return myLogger;
            }
        }

        public object ConfigurationManager { get; }

        public void LogError(string message, Exception ex)
        {
            log4NetLogger.Error(message, ex);
        }
        public void LogErrorMessage(string format, string message)
        {
            log4NetLogger.Error(String.Format(format, message));
        }

        public void LogInfoMessage(string format, string message)
        {
            log4NetLogger.Info(String.Format(format, message));
        }
    }
}

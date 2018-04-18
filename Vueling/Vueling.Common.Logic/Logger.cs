using System;


[assembly: log4net.Config.XmlConfigurator(Watch = true, ConfigFile = "log4net.config")]
namespace Vueling.Common.Logic
{
    public sealed class Logger : ILogger
    {
        private readonly log4net.ILog Log;
        private readonly bool isDebugEnabled = true;
        private readonly bool isErrorEnabled = true;

        public TimeSpan ExecutionTime { get; set; }
        public int counter { get; set; }

        public Logger(Type tipo)
        {
            Log = log4net.LogManager.GetLogger(tipo);
        }

        public void Debug(string message)
        {
            if(isDebugEnabled) Log.Debug(message);
        }

        public void Error(string message)
        {
            if(isErrorEnabled) Log.Error(message);
        }

        public void Exception(Exception exception, string message)
        {
            Log.Error(message, exception);
        }

        public void Exception(Exception exception)
        {
            Log.Error(exception);
        }
    }
}

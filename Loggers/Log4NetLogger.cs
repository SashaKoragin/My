using System;
using NLog;

namespace Loggers
{
    public class Log4NetLogger
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void Trace(Exception ex)
        {
            logger.Trace(ex);
        }

        public static void Debug(Exception ex)
        {
            logger.Debug(ex);
        }
        public static void Info(Exception ex)
        {
            logger.Info(ex);
        }
        public static void Warn(Exception ex)
        {
            logger.Warn(ex);
        }
        public static void Error(Exception ex)
        {
            logger.Error(ex);
        }
        public static void Fatal(Exception ex)
        {
            logger.Fatal(ex);
        }
    }
}

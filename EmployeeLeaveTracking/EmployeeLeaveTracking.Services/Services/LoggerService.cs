using NLog;

namespace EmployeeLeaveTracking.Services.Services;

public class LoggerService : Interfaces.ILogger
{
    private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();

    public LoggerService() { }

    public void LogDebug(string message)
    {
        logger.Debug(message);
    }
    public void LogError(string message)
    {
        logger.Error(message);
    }
    public void LogInfo(string message)
    {
        logger.Info(message);
    }
    public void LogWarn(string message)
    {
        logger.Warn(message);
    }
}

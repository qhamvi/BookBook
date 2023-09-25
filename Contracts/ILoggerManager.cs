
namespace Contracts
{
    public interface ILoggerManager 
    {
        void LogInfor(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);

    }
}
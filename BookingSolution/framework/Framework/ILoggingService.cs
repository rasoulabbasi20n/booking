using Framework.Problems;

namespace Framework
{
    public interface ILoggingService
    {
        void Info(string message);
        void Error(ProblemBase problem);
        void Error(Exception exception);
        void Error(string message);
    }
}

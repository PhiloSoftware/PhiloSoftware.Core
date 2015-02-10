using System;

namespace PhiloSoftware.Core.Infrastructure.Services.Logging
{
    public interface ILogger
    {
        void Info(string message, params object[] args);
        void Warning(string message, params object[] args);
        void Error(Exception exception, string message, params object[] args);
    }
}

using System;
using System.Runtime.CompilerServices;
using Advice.Logging.Models;

namespace Advice.Logging.Contracts
{
    public interface IElkLog
    {
        void LogException(ExceptionLogRequest exceptionLogRequest,
           [CallerMemberName] string callerMemberName = "",
           [CallerFilePath] string sourceFilePath = "",
           [CallerLineNumber] int sourceLineNumber = 0);

        void Info(InfoLogRequest infoLog,
           [CallerMemberName] string callerMemberName = "",
           [CallerFilePath] string sourceFilePath = "",
           [CallerLineNumber] int sourceLineNumber = 0);
    }
}

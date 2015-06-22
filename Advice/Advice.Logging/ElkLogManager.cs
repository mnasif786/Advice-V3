using System;
using Advice.Logging.Contracts;
using Advice.Logging.Implementations;

namespace Advice.Logging
{
    public static class ElkLogManager
    {
        public static IElkLog GetLogger(Type type)
        {
           return new ElkLog(type);
        }
    }
}

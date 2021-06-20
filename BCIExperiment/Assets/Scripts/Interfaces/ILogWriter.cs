using System.Collections.Generic;
using Model;

namespace Interfaces
{
    public interface ILogWriter
    {
        void PostDataToLogfile(LogEntry logEntry);
    }
}
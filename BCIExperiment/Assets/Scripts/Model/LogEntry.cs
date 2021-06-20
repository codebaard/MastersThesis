using System;
using Interfaces;

namespace Model
{
    public abstract class LogEntry : IDisposable
    {
        public DateTime timestamp ;
        public string EventType;

        protected virtual void setTimestamp()
        {
            this.timestamp = DateTime.Now;
        }

        public virtual string getLogString()
        {
            return timestamp.ToString() + "," + EventType;
        }

        public void Dispose()
        {
            //nothing really happens here anyway
        }
    }
}
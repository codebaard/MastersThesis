using System;
using Interfaces;

namespace Model
{
    public abstract class LogEntry : IDisposable
    {
        public long timestamp ;
        public string EventType;

        protected virtual void setTimestamp()
        {
            
        }
        public virtual string getLogString()
        {
            this.timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            return timestamp + ";" + EventType;
        }

        public void Dispose()
        {
            //nothing really happens here anyway
        }
    }
}
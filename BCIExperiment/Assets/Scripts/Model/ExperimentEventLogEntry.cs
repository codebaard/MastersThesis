using System;

namespace Model
{
    public class ExperimentEventLogEntry : LogEntry
    {
        protected string message;

        protected ExperimentEventLogEntry() { }
        public ExperimentEventLogEntry(string message)
        {
            this.EventType = typeof(ExperimentEventLogEntry).ToString();
            this.message = message;
        }
        public override string getLogString()
        {
            return base.getLogString() + ";" + message;
        }
    }
}
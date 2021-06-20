using System;

namespace Model
{
    public class NeuroTagConfidenceLogEntry : LogEntry
    {
        public float value;
        
        public NeuroTagConfidenceLogEntry(float value)
        {
            this.value = value;
            this.EventType = typeof(NeuroTagConfidenceLogEntry).ToString();
        }

        public override string getLogString()
        {
            return base.getLogString() + "," + value;
        }
    }
}
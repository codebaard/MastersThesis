using System;

namespace Model
{
    public class NeuroTagConfidenceLogEntry : LogEntry
    {
        public float Value;

        public NeuroTagConfidenceLogEntry(float value)
        {
            this.Value = value;
            this.EventType = typeof(NeuroTagConfidenceLogEntry).ToString();
        }

        public override string getLogString()
        {
            return base.getLogString() + ";" + Value;
        }
    }
}
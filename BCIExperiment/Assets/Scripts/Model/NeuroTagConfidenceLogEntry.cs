using System;

namespace Model
{
    public class NeuroTagConfidenceLogEntry : LogEntry
    {
        public float Value;
        public int Index;

        public NeuroTagConfidenceLogEntry(float value, int index)
        {
            Value = value;
            Index = index;
            EventType = typeof(NeuroTagConfidenceLogEntry).ToString();
        }

        public override string getLogString()
        {
            return base.getLogString() + ";" + Index + ";" + Value;
        }
    }
}
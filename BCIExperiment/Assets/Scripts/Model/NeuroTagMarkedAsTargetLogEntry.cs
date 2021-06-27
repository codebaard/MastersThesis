namespace Model
{
    public class NeuroTagMarkedAsTargetLogEntry : LogEntry
    {
        public int index;
        public NeuroTagMarkedAsTargetLogEntry(int index)
        {
            this.index = index;
            this.EventType = typeof(NeuroTagMarkedAsTargetLogEntry).ToString();
        }
        
        public override string getLogString()
        {
            return base.getLogString() + ";" + index;
        }
    }
}
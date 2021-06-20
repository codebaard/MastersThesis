namespace Model
{
    public class NeuroTagMarkedAsTargetLogEntry : LogEntry
    {
        public string name;
        public NeuroTagMarkedAsTargetLogEntry(string name)
        {
            this.name = name;
            this.EventType = typeof(NeuroTagMarkedAsTargetLogEntry).ToString();
        }
        
        public override string getLogString()
        {
            return base.getLogString() + ";" + name;
        }
    }
}
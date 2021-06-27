namespace Model
{
    public class NeuroTagHitLogEntry : LogEntry
    {
        public NeuroTagHitLogEntry() { }
        public int TargetIndex;

        public NeuroTagHitLogEntry(int index)
        {
            this.TargetIndex = index;
            this.EventType = typeof(NeuroTagHitLogEntry).ToString();
        }

        public override string getLogString()
        {
            return base.getLogString() + ";" + TargetIndex;
        }
    }
}
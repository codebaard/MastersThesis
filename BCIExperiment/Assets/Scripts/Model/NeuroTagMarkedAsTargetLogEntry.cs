namespace Model
{
    public class NeuroTagMarkedAsTargetLogEntry : NeuroTagHitLogEntry
    {
        public NeuroTagMarkedAsTargetLogEntry(int index)
        {
            this.TargetIndex = index;
            this.EventType = typeof(NeuroTagMarkedAsTargetLogEntry).ToString();
        }
    }
}
using NextMind.Events;

namespace Model
{
    public class NeuroTagHitLogEntry : LogEntry
    {
        public int TargetIndex;

        public NeuroTagHitLogEntry(int index)
        {
            this.TargetIndex = index;
            this.EventType = typeof(NeuroTagHitLogEntry).ToString();
        }

        public override string toString()
        {
            return base.toString() + "," + TargetIndex;
        }
    }
}
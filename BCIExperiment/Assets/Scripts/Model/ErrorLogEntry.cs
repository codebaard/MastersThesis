namespace Model
{
    public class ErrorLogEntry : ExperimentEventLogEntry
    {
        public ErrorLogEntry(string message)
        {
            this.EventType = typeof(ErrorLogEntry).ToString();
            this.message = message;
        }
    }
}
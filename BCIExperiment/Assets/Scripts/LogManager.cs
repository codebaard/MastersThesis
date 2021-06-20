using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Model;
using UnityEngine;
using NextMind.Events;

public class LogManager : MonoBehaviour, ILogWriter
{
    #region NeuroTag events

    public void OnTriggered(int index)
    {
        using (NeuroTagHitLogEntry neuroTagHitLogEntry = new NeuroTagHitLogEntry(index))
        {
            PostDataToLogfile(neuroTagHitLogEntry);           
        }
    }
    
    public void OnConfidenceChanged(float value)
    {
        using (NeuroTagConfidenceLogEntry neuroTagConfidenceLogEntry = new NeuroTagConfidenceLogEntry(value))
        {
            PostDataToLogfile(neuroTagConfidenceLogEntry);           
        }
    }

    #endregion
    
    public void PostDataToLogfile(LogEntry logEntry)
    {
        print(logEntry.ToString());
    }
}

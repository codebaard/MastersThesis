using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Model;
using UnityEngine;
using NextMind.Events;

public class LogManager : MonoBehaviour, ILogWriter
{

    public LogManager()
    {
        Debug.Log("I'm actually Alive!");
    }

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

    public void OnTargetSet(int index)
    {
        using (NeuroTagMarkedAsTargetLogEntry
            neuroTagMarkedAsTargetLogEntry = new NeuroTagMarkedAsTargetLogEntry(index))
        {
            PostDataToLogfile(neuroTagMarkedAsTargetLogEntry);
        }
    }

    public void onExperimentFinished()
    {
        //save log
    }
    
    public void PostDataToLogfile(LogEntry logEntry)
    {
        Debug.Log(logEntry.getLogString());
    }
}

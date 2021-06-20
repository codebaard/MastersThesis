using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Interfaces;
using Model;
using UnityEngine;
using NextMind.Events;
using DefaultNamespace;

public class LogManager : MonoBehaviour, ILogWriter
{
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

    private void OnEnable()
    {
        ExperimentManager.onExperimentStarted += onExperimentStarted;
        ExperimentManager.onTargetSet += OnTargetSet;
    }

    private void OnDisable()
    {
        ExperimentManager.onExperimentStarted -= onExperimentStarted;
        ExperimentManager.onTargetSet -= OnTargetSet;
    }

    void onExperimentStarted()
    {
        //OpenLogForWriting("4711");
    }
    
    public void PostDataToLogfile(LogEntry logEntry)
    {
        //Debug.Log(logEntry.getLogString());
    }

    public void OpenLogForWriting(string subject)
    {
        print(subject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using Interfaces;
using Model;
using UnityEngine;
using NextMind.Events;
using NextMind;
using DefaultNamespace;
using NextMind.Devices;
using NextMind.NeuroTags;

public class LogManager : MonoBehaviour, ILogWriter
{
    private StreamWriter _streamWriter;
    private NeuroTag _currentNeuroTag;
    private NeuroTagIdentifier _identifier;

    [SerializeField]
    private bool _enableConfidenceLogging;

    private void _subscribeToNeuroTag(GameObject neurotag)
    {
        _unsubscribeFromNeuroTag();
        _currentNeuroTag = neurotag.GetComponent<NeuroTag>();
        _identifier = neurotag.GetComponent<NeuroTagIdentifier>();
        
        _currentNeuroTag.onConfidenceChanged.AddListener(OnConfidenceChanged);
        _currentNeuroTag.onTriggered.AddListener(OnTriggered);        
    }

    private void _unsubscribeFromNeuroTag()
    {
        _currentNeuroTag.onConfidenceChanged.RemoveListener(OnConfidenceChanged);
        _currentNeuroTag.onTriggered.RemoveListener(OnTriggered);
        _currentNeuroTag = null;
        _identifier = null;
    }
    
    // works
    public void OnTriggered()
    {
        using (NeuroTagHitLogEntry neuroTagHitLogEntry = new NeuroTagHitLogEntry(_identifier.getIndex()))
        {
            PostDataToLogfile(neuroTagHitLogEntry);           
        }
    }
    public void OnConfidenceChanged(float value)
    {
        using (NeuroTagConfidenceLogEntry msg = new NeuroTagConfidenceLogEntry(value, _identifier.getIndex()))
        {
            if(_enableConfidenceLogging)
                PostDataToLogfile(msg);           
        }
    }
    void onExperimentStarted(string message)
    {
        using (ExperimentEventLogEntry msg = new ExperimentEventLogEntry(message))
        {
            PostDataToLogfile(msg);
        }
    }

    void onExperimentFinished(string message)
    {
        using (ExperimentEventLogEntry msg = new ExperimentEventLogEntry(message))
        {
            PostDataToLogfile(msg);
        }
        _streamWriter.Flush();
        _streamWriter.Close();
    }
    public void OnTargetSet(GameObject neurotag)
    {
        _subscribeToNeuroTag(neurotag);
        
        using (NeuroTagMarkedAsTargetLogEntry msg = new NeuroTagMarkedAsTargetLogEntry(_identifier.getIndex()))
        {
            PostDataToLogfile(msg);
        }
    }

    public void LogDeviceQualityReadings(Device sensor)
    {
        using (SensorTelemetryLogEntry msg = new SensorTelemetryLogEntry(sensor))
        {
            PostDataToLogfile(msg);
        }
    }
    private void OnEnable()
    {
        OpenLogForWriting();
        ExperimentManager.onExperimentStarted += onExperimentStarted;
        ExperimentManager.onExperimentFinished += onExperimentFinished;
        TargetManager.onTargetSet += OnTargetSet;
    }

    private void OnDisable()
    {
        ExperimentManager.onExperimentStarted -= onExperimentStarted;
        ExperimentManager.onExperimentFinished -= onExperimentFinished;
        TargetManager.onTargetSet -= OnTargetSet;
    }
    public void PostDataToLogfile(LogEntry logEntry)
    {
        Debug.Log(logEntry.getLogString());
        if (!_streamWriter.Equals(default))
        {
            _streamWriter.WriteLineAsync(logEntry.getLogString());           
        }
    }
    public void OpenLogForWriting()
    {
        string filepath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string filename = "BCILog.csv";

        try
        {
            _streamWriter = new StreamWriter(Path.Combine(filepath, filename), true);
        }
        catch (Exception e)
        {
            using (ErrorLogEntry msg = new ErrorLogEntry("streamwriter error: " + e.Message))
            {
                PostDataToLogfile(msg);
            }
        }
    }
}

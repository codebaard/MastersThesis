using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    [SerializeField] private bool _enableConfidenceLogging; //during development for less clutter in the logs

    private void _onExperimentStarted(string message)
    {
        Device sensor = NeuroManager.Instance.Devices.FirstOrDefault();
        PostDataToLogfile(new SensorTelemetryLogEntry(sensor));
        PostDataToLogfile(new ExperimentEventLogEntry(message));
    }

    private void _onExperimentFinished(string message)
    {
        PostDataToLogfile(new ExperimentEventLogEntry(message));
    }
    private void _onTargetSet(GameObject neurotag)
    {
        PostDataToLogfile(new NeuroTagMarkedAsTargetLogEntry(
                neurotag.GetComponent<NeuroTagLogWrapper>().getNeuroTagIndex()));
    }

    public void LogDeviceQualityReadings(Device sensor)
    {
        PostDataToLogfile(new SensorTelemetryLogEntry(sensor));
    }
    private void OnEnable()
    {
        OpenLogForWriting();
        ExperimentManager.onExperimentStarted += _onExperimentStarted;
        ExperimentManager.onExperimentFinished += _onExperimentFinished;
        TargetManager.onTargetSet += _onTargetSet;
    }

    private void OnDisable()
    {
        ExperimentManager.onExperimentStarted -= _onExperimentStarted;
        ExperimentManager.onExperimentFinished -= _onExperimentFinished;
        TargetManager.onTargetSet -= _onTargetSet;
        
        _streamWriter.Flush();
        _streamWriter.Close();
    }
    public void PostDataToLogfile(LogEntry logEntry)
    {
        Debug.Log(logEntry.getLogString());
        if (!_streamWriter.Equals(default))
        {
            _streamWriter.WriteLine(logEntry.getLogString());
            _streamWriter.Flush();
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

    public bool LogConfidenceData()
    {
        return _enableConfidenceLogging;
    }
}

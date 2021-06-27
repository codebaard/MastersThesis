using System;
using System.IO;
using System.Linq;
using DefaultNamespace;
using Interfaces;
using Model;
using UnityEngine;
using NextMind;
using NextMind.Devices;
using UnityEngine.UI;

public class LogManager : MonoBehaviour, ILogWriter
{
    private StreamWriter _streamWriter;
    [SerializeField] private bool _enableConfidenceLogging; //during development for less clutter in the logs
    [SerializeField] private GameObject _participationNumber;

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
        ExperimentManager.onExperimentStarted += _onExperimentStarted;
        ExperimentManager.onExperimentFinished += _onExperimentFinished;
        TargetManager.onTargetSet += _onTargetSet;
        UIManager.onParticipantNumberEntered += OpenLogForWriting;
    }

    private void OnDisable()
    {
        ExperimentManager.onExperimentStarted -= _onExperimentStarted;
        ExperimentManager.onExperimentFinished -= _onExperimentFinished;
        TargetManager.onTargetSet -= _onTargetSet;
        UIManager.onParticipantNumberEntered -= OpenLogForWriting;
        
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
        string participant = _participationNumber.GetComponent<Text>().text;
        string filepath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string filename = "BCILog_" + participant + ".csv";

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

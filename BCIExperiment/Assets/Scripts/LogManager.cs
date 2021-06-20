using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Model;
using UnityEngine;

public class LogManager : MonoBehaviour, ILogWriter
{
    public void PostDataToLogfile(AbstractLogEntry logEntry)
    {
        print(logEntry.TargetName);
    }
}

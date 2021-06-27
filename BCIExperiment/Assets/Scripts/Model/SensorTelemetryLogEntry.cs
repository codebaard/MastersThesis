using System;
using System.Collections.Generic;
using NextMind.Calibration;
using NextMind.Devices;

namespace Model
{
    public class SensorTelemetryLogEntry : LogEntry
    {
        private CalibrationResults _calibrationResults;
        private List<Single> _contactQuality;

        public SensorTelemetryLogEntry(Device sensor)
        {
            _calibrationResults = sensor.GetCalibrationResults();
            _contactQuality = new List<float>();

            for (uint i = 0; i < 8; i++)
            {
                _contactQuality.Add(sensor.GetContact(i));
            }

            EventType = typeof(SensorTelemetryLogEntry).ToString();
        }

        public override string getLogString()
        {
            return base.getLogString() + _qualityReadings() + _calibrationResults.Grade;
        }

        private string _qualityReadings()
        {
            string readings = "";

            foreach (Single result in _contactQuality)
            {
                readings += result + ";";
            }

            return readings;
        }
    }
}
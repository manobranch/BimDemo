using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace productuploader.Logic
{
    public class InsightsLogic
    {
        static readonly Microsoft.ApplicationInsights.TelemetryClient telemetry = new Microsoft.ApplicationInsights.TelemetryClient();

        public static void LogInfo(string logText)
        {
            Log(LogLevel.INFO, logText, null);
        }

        public static void LogError(string logText, Exception e)
        {
            Log(LogLevel.ERROR, logText, e);
        }

        private static void Log(LogLevel level, string logText, Exception e)
        {
            string textToLog = BuildLogText(level, logText, e);

            telemetry.TrackTrace(textToLog, Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Information);
            telemetry.TrackEvent($"Tracking Event: {logText}");
            telemetry.TrackMetric($"Tracring Metric: {logText}", 777);

            telemetry.Flush();
        }

        private static string BuildLogText(LogLevel level, string logText, Exception e)
        {
            string returnString = "";
            returnString += $"Level {level}. Text: {logText} \n\n";

            if (e != null)
            {
                string inner = e.InnerException != null ? e.InnerException.ToString() : "NULL";

                returnString += $"Exception: {e.Message} Inner exception: {inner}";
            }

            return returnString;
        }

        private enum LogLevel
        {
            INFO,
            ERROR
        }
    }
}
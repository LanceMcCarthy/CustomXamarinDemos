using System;

namespace CallDetector.Portable.Common
{
    public class UtcTimestamper
    {
        readonly DateTime _startTime;

        public UtcTimestamper()
        {
            _startTime = DateTime.UtcNow;
        }

        public string GetFormattedTimestamp()
        {
            TimeSpan duration = DateTime.UtcNow.Subtract(_startTime);
            return $"Service started at {_startTime} ({duration:c} ago).";
        }
    }
}

using System;

namespace CallDetector.Portable.Common
{
    public class CallStateChangedEventArgs : EventArgs
    {
        public string PhoneNumber { get; set; }

        // "Idle" "Offhook" "Ringing"
        public string CallState { get; set; }
    }
}
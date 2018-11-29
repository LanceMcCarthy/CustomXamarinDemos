using System;
using Android.Telephony;
using CallDetector.Portable.Common;

namespace CallDetector.Android.Listeners
{
    public class CustomPhoneStateListener : PhoneStateListener
    {
        private readonly EventHandler<CallStateChangedEventArgs> _stateChangedHandler;

        public CustomPhoneStateListener(EventHandler<CallStateChangedEventArgs> handler)
        {
            this._stateChangedHandler = handler;
        }

        public override void OnCallStateChanged(CallState state, string incomingNumber)
        {
            _stateChangedHandler?.Invoke(this, new CallStateChangedEventArgs
            {
                CallState = state.ToString(),
                PhoneNumber = incomingNumber
            });
            
            base.OnCallStateChanged(state, incomingNumber);
        }
    }
}
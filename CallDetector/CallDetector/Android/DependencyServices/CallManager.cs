using System;
using Android.App;
using Android.Content;
using Android.Telephony;
using CallDetector.Android.Listeners;
using CallDetector.Portable.Common;
using CallDetector.Portable.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(CallDetector.Android.DependencyServices.CallManager))]
namespace CallDetector.Android.DependencyServices
{
    // See Note in StartService about GetSystemService needing Activity
    public class CallManager : ICallManager
    {
        private TelephonyManager _telephonyManager;
        private PhoneStateListener _listener;

        public CallManager()
        {
            System.Diagnostics.Debug.WriteLine("CallManager Instantiated");
            // todo Think about what else needs to be initialized
        }

        public event EventHandler<CallStateChangedEventArgs> CallStatedChanged;

        public void StartService()
        {
            System.Diagnostics.Debug.WriteLine("StartService started...");

            // Possible Bug - GetSystemService() is only available through the Activity base class. seek alternatives.
            _telephonyManager = (TelephonyManager)Application.Context.GetSystemService(Context.TelephonyService);

            // I'm passing the event handler reference to the Listener so that it can invoke the event when the state changes
            _listener = new CustomPhoneStateListener(this.CallStatedChanged);

            // Note: manifest must have READ_PHONE_STATE permission
            // I assign the listener to the manager for CallState (note: manifest must have Call_STATE permission))
            _telephonyManager.Listen(_listener, PhoneStateListenerFlags.CallState);

            System.Diagnostics.Debug.WriteLine("StartService ended.");
        }

        public void StopService()
        {
            System.Diagnostics.Debug.WriteLine("StopService started...");

            _telephonyManager.Dispose();
            _listener.Dispose();

            _telephonyManager = null;
            _listener = null;

            System.Diagnostics.Debug.WriteLine("StopService ended.");
        }
    }
}
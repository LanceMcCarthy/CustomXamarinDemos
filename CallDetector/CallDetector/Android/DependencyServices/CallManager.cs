using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Telephony;
using CallDetector.Android.Listeners;
using CallDetector.Portable.Common;
using CallDetector.Portable.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(CallDetector.Android.DependencyServices.CallManager))]
namespace CallDetector.Android.DependencyServices
{
    public class CallManager : ICallManager
    {
        private TelephonyManager _telephonyManager;
        private PhoneStateListener _listener;

        public event EventHandler<CallStateChangedEventArgs> CallStatedChanged;

        public void StartService()
        {
            _telephonyManager = (TelephonyManager)Application.Context.GetSystemService(Context.TelephonyService);
            _listener = new CustomPhoneStateListener(this.CallStatedChanged);
            
            // Note: manifest must have READ_PHONE_STATE permission
            _telephonyManager.Listen(_listener, PhoneStateListenerFlags.CallState);
        }

        public void StopService()
        {
            _telephonyManager.Dispose();
            _listener.Dispose();

            _telephonyManager = null;
            _listener = null;
        }
        
        public void DeclineCall()
        {
            // Credit https://stackoverflow.com/a/17538956/1406210
            var telephonyManagerGetITelephony = JNIEnv.GetMethodID(
                _telephonyManager.Class.Handle,
                "getITelephony",
                "()Lcom/android/internal/telephony/ITelephony;");

            var telephony = JNIEnv.CallObjectMethod(_telephonyManager.Handle, telephonyManagerGetITelephony);
            var telephonyClass = JNIEnv.GetObjectClass(telephony);
            var telephonyEndCallMethod = JNIEnv.GetMethodID(telephonyClass, "endCall", "()Z");

            JNIEnv.CallBooleanMethod(telephony, telephonyEndCallMethod);

            JNIEnv.DeleteLocalRef(telephony);
            JNIEnv.DeleteLocalRef(telephonyClass);
        }
    }
}
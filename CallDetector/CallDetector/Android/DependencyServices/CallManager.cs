using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telecom;
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

        public bool DeclineCall()
        {
            var success = false;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
            {
                //Available in API 28, found it here https://developer.android.com/reference/android/telecom/TelecomManager#endCall()
                // permissions required ANSWER_PHONE_CALLS
                var tm = (TelecomManager)Application.Context.GetSystemService(Context.TelecomService);
                success = tm.EndCall();
            }
            else
            {
                // Credit https://stackoverflow.com/a/17538956/1406210
                var telephonyManagerGetITelephony = JNIEnv.GetMethodID(
                    _telephonyManager.Class.Handle,
                    "getITelephony",
                    "()Lcom/android/internal/telephony/ITelephony;");

                var telephony = JNIEnv.CallObjectMethod(_telephonyManager.Handle, telephonyManagerGetITelephony);
                var telephonyClass = JNIEnv.GetObjectClass(telephony);
                var telephonyEndCallMethod = JNIEnv.GetMethodID(telephonyClass, "endCall", "()Z");

                success = JNIEnv.CallBooleanMethod(telephony, telephonyEndCallMethod);

                JNIEnv.DeleteLocalRef(telephony);
                JNIEnv.DeleteLocalRef(telephonyClass);
            }

            return success;
        }
    }
}
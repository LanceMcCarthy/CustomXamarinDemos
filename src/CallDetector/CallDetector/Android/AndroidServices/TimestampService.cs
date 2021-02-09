using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using CallDetector.Portable.Common;

// NOTE: Not complete, it will likely break the app if you tried to use it
// This is a placeholder for when I need to build the background service.

namespace CallDetector.Android.AndroidServices
{
    [Service]
    public class TimestampService : Service
    {
        private readonly string _tag = typeof(TimestampService).FullName;
        private const int DelayBetweenLogMessages = 5000; //ms
        private const int NotificationId = 10000;

        private UtcTimestamper _timeStamper;
        private bool _isStarted;
        private Handler _handler;
        private Action _runnable;

        public override void OnCreate()
        {
            base.OnCreate();
            Log.Info(_tag, "OnCreate: the service is initializing.");

            _timeStamper = new UtcTimestamper();
            _handler = new Handler();

            // This Action is only for demonstration purposes.
            _runnable = new Action(() =>
            {
                if (_timeStamper != null)
                {
                    Log.Debug(_tag, _timeStamper.GetFormattedTimestamp());
                    _handler.PostDelayed(_runnable, DelayBetweenLogMessages);
                }
            });
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            if (_isStarted)
            {
                Log.Info(_tag, "OnStartCommand: This service has already been started.");
            }
            else
            {
                Log.Info(_tag, "OnStartCommand: The service is starting.");
                DispatchNotificationThatServiceIsRunning();
                _handler.PostDelayed(_runnable, DelayBetweenLogMessages);
                _isStarted = true;
            }

            // This tells Android not to restart the service if it is killed to reclaim resources.
            return StartCommandResult.NotSticky;
        }
        
        public override IBinder OnBind(Intent intent)
        {
            // Return null because this is a pure started service. A hybrid service would return a binder that would allow access to the GetFormattedStamp() method.
            return null;
        }
        
        public override void OnDestroy()
        {
            // We need to shut things down.
            Log.Debug(_tag, GetFormattedTimestamp());
            Log.Info(_tag, "OnDestroy: The started service is shutting down.");

            // Stop the handler.
            _handler.RemoveCallbacks(_runnable);

            // Remove the notification from the status bar.
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Cancel(NotificationId);

            _timeStamper = null;
            _isStarted = false;
            base.OnDestroy();
        }
        
        string GetFormattedTimestamp()
        {
            return _timeStamper?.GetFormattedTimestamp();
        }

        void DispatchNotificationThatServiceIsRunning()
        {
            var notificationBuilder = new Notification.Builder(this)
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentTitle(Resources.GetString(Resource.String.app_name))
                .SetContentText(Resources.GetString(Resource.String.notification_text));
            
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Notify(NotificationId, notificationBuilder.Build());
        }
    }
}
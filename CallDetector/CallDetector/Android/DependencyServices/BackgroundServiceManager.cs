using Android.Content;
using CallDetector.Android.AndroidServices;
using CallDetector.Portable.DependencyServices;
using Application = Android.App.Application;

// NOTE: Not complete, I'd be surprised if this didn't break the app.
// This is a placeholder for when I need to build the background service.

//[assembly: Xamarin.Forms.Dependency(typeof(CallDetector.Android.DependencyServices.BackgroundServiceManager))]
namespace CallDetector.Android.DependencyServices
{
    public class BackgroundServiceManager : IBackgroundServiceManager
    {
        Intent _serviceToStart;

        public void StartService()
        {
            _serviceToStart = new Intent(Application.Context, typeof(TimestampService));

            Application.Context.StartService(_serviceToStart);
        }

        public void StopService()
        {
            Application.Context.StopService(_serviceToStart);
        }
    }
}
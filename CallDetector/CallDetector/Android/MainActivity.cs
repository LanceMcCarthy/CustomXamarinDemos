using Android.App;
using Android.Content.PM;
using Android.OS;
using CallDetector.Android.DependencyServices;

namespace CallDetector.Android
{
    [Activity(Label = "CallDetector.Android", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
    
            base.OnCreate(savedInstanceState);
    
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Xamarin.Forms.DependencyService.Register<CallManager>();
            //Xamarin.Forms.DependencyService.Register<BackgroundServiceManager>();

            LoadApplication(new Portable.App());
        }
    }
}
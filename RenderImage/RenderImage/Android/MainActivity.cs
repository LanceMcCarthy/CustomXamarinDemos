using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.CurrentActivity;
using RenderImage.Android.Services;
using Xamarin.Forms;

namespace RenderImage.Android
{
    [Activity(Label = "RenderImage.Android", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
    
            base.OnCreate(savedInstanceState);
            
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            DependencyService.Register<RenderService>();
            DependencyService.Register<HingeService>();
            DependencyService.Register<LayoutService>();

            HingeService.MainActivity = this;

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            LoadApplication(new Portable.App());
        }
    }
}
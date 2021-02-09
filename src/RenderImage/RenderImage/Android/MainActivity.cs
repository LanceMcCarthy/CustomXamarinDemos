using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Plugin.CurrentActivity;
using RenderImage.Android.Services;
using Xamarin.Forms;

namespace RenderImage.Android
{
    [Activity(Label = "RenderImage.Android", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
    
            base.OnCreate(bundle);
            
            global::Xamarin.Forms.Forms.Init(this, bundle);

            // ************ NOTE *************** //

            DependencyService.Register<RenderService>();

            // ********************************* //
            
            CrossCurrentActivity.Current.Init(this, bundle);

            LoadApplication(new Portable.App());
        }
    }
}
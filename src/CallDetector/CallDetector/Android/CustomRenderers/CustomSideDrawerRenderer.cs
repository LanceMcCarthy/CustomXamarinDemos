using Android.Content;
using Android.Graphics.Drawables;
using Android.Renderscripts;
using Com.Telerik.Android.Primitives.Widget.Sidedrawer;
using Com.Telerik.Android.Primitives.Widget.Sidedrawer.Transitions;
using Telerik.XamarinForms.PrimitivesRenderer.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RadSideDrawer), typeof(CallDetector.Android.CustomRenderers.CustomSideDrawerRenderer))]
namespace CallDetector.Android.CustomRenderers
{
    public class CustomSideDrawerRenderer : SideDrawerRenderer
    {
        public CustomSideDrawerRenderer(Context context) : base(context) { }

        protected override IDrawerFadeLayer CreateFadeLayer()
        {
            // Read the color out of the XF App class
            var themeColor = (Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["LightBlueColor"];

            // TODO Ask team if the RenderScript isn't needed any more.
            //var rs = RenderScript.Create(this.Context);

            return new BlurFadeLayer(this.Context)
            {
                Background = new ColorDrawable(themeColor.ToAndroid())
            };
        }

        protected override IDrawerTransition CreateCustomTransition()
        {
            return new FallDownTransition();
        }
    }
}
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("MyCompany")]
[assembly: ExportEffect(typeof(DrawerDismissEffect.Android.Effects.DrawerEffect), "DrawerEffect")]
namespace DrawerDismissEffect.Android.Effects
{
    public class DrawerEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Element.Effects.FirstOrDefault(e => e is Portable.Effects.DrawerEffect) is Portable.Effects.DrawerEffect effect)
            {
                if (Control is Com.Telerik.Android.Primitives.Widget.Sidedrawer.RadSideDrawer drawer)
                {
                    drawer.TapOutsideToClose = effect.TapOutsideToClose;
                }
            }
        }

        protected override void OnDetached()
        {
        }
    }
}

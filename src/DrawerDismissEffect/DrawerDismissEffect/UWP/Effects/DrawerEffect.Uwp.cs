using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ResolutionGroupName("MyCompany")]
[assembly: ExportEffect(typeof(DrawerDismissEffect.UWP.Effects.DrawerEffect), "DrawerEffect")]
namespace DrawerDismissEffect.UWP.Effects
{
    public class DrawerEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Element.Effects.FirstOrDefault(e => e is Portable.Effects.DrawerEffect) is Portable.Effects.DrawerEffect effect)
            {
                if (Control is Telerik.UI.Xaml.Controls.Primitives.RadSideDrawer drawer)
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

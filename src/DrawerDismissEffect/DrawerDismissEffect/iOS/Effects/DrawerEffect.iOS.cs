using System.Linq;
using TelerikUI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("MyCompany")]
[assembly: ExportEffect(typeof(DrawerDismissEffect.iOS.Effects.DrawerEffect), "DrawerEffect")]
namespace DrawerDismissEffect.iOS.Effects
{
    public class DrawerEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Element.Effects.FirstOrDefault(e => e is Portable.Effects.DrawerEffect) is Portable.Effects.DrawerEffect effect)
            {
                if (Control is TKSideDrawerView drawerView)
                {
                    drawerView.DefaultSideDrawer.AllowGestures = effect.TapOutsideToClose;
                }
            }
        }

        protected override void OnDetached()
        {
        }
    }
}
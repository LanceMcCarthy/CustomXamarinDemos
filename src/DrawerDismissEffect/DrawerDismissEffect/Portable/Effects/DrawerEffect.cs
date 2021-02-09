using Xamarin.Forms;

namespace DrawerDismissEffect.Portable.Effects
{
    public class DrawerEffect : RoutingEffect
    {
        public DrawerEffect() 
            : base("MyCompany.DrawerEffect")
        {
        }

        public bool TapOutsideToClose { get; set; }
    }
}

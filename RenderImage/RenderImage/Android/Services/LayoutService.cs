using RenderImage.Android.Services;
using RenderImage.Portable.Controls.LayoutService;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(LayoutService))]
namespace RenderImage.Android.Services
{
    public class LayoutService : LayoutServiceBase, ILayoutService
    {
        public override Point? GetLocationOnScreen(VisualElement visualElement)
        {
            var view = Platform.GetRenderer(visualElement);

            if (view?.View == null)
                return null;

            int[] location = new int[2];
            view.View.GetLocationOnScreen(location);
            return new Xamarin.Forms.Point(view.View.Context.FromPixels(location[0]), view.View.Context.FromPixels(location[1]));
        }
    }
}
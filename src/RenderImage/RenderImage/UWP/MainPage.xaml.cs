using RenderImage.UWP.Services;
using Xamarin.Forms;

namespace RenderImage.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            
            LoadApplication(new Portable.App());
        }
    }
}

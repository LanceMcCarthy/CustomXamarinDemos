using CustomSeriesLabels.Portable.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomSeriesLabels.Portable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadialPage : ContentPage
    {
        public RadialPage()
        {
            InitializeComponent();
            BindingContext = new RadialViewModel();
        }
    }
}
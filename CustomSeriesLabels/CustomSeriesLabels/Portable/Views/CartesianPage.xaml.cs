using CustomSeriesLabels.Portable.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomSeriesLabels.Portable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartesianPage : ContentPage
    {
        public CartesianPage()
        {
            InitializeComponent();
            BindingContext = new BarViewModel();
        }
    }
}
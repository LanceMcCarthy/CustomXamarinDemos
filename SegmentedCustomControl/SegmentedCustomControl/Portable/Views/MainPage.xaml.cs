using SegmentedCustomControl.Portable.ViewModels;
using Xamarin.Forms;

namespace SegmentedCustomControl.Portable.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
    }
}

using CustomSeriesLabels.Portable.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomSeriesLabels.Portable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnotationsPage : ContentPage
    {
        public AnnotationsPage()
        {
            InitializeComponent();
            BindingContext = new AnnotationsViewModel();
        }
    }
}
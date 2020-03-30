using System;
using CustomSeriesLabels.Portable.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CustomSeriesLabels.Portable.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void CartesianButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartesianPage());
        }

        private async void RadialButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RadialPage());
        }
    }
}

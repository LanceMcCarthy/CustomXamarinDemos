using System;
using CommonHelpers.Extensions;
using RenderImage.Portable.Models;
using RenderImage.Portable.Services;
using Xamarin.Forms;

namespace RenderImage.Portable.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void RenderButton_OnClicked(object sender, EventArgs e)
        {
            var imageBytes = await DependencyService.Get<IRenderService>().RenderAsync(RenderEncodingOptions.Jpeg);

            if (imageBytes == null)
            {
                await DisplayAlert("Error", "Could not render the image.", "ok");
            }
            else
            {
                await imageBytes.SaveToLocalFolderAsync(App.CapturedImageFilePath);

                await Navigation.PushAsync(new ImgEditorPage());
            }
        }
    }
}

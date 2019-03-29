using System;
using System.Diagnostics;
using RenderImage.Portable.Services;
using Xamarin.Forms;

namespace RenderImage.Portable
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void RenderAll_OnClicked(object sender, EventArgs e)
        {
            var imageBytes = await DependencyService.Get<IRenderService>().RenderAsync();

            if (imageBytes == null)
            {
                throw new NullReferenceException("Image was not rendered");
            }

            await Navigation.PushAsync(new ResultsPage(imageBytes) { Title = "Full Page Result"});
        }

        private async void RenderCropAbsolute_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var x = int.Parse(XEntry.Text);
                var y = int.Parse(YEntry.Text);
                var width = int.Parse(WidthEntry.Text);
                var height = int.Parse(HeightEntry.Text);
                
                var imageBytes = await DependencyService.Get<IRenderService>().RenderAsync(x, y, width, height);
                
                if (imageBytes == null)
                {
                    throw new NullReferenceException("Image was not rendered, the DependencyService returned null. Check the cropping dimensions and try again");
                }

                await Navigation.PushAsync(new ResultsPage(imageBytes) { Title = "Absolute Result" });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"{ex.Message}", "OK");
            }
        }

        private async void RenderCropProportional_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var x = int.Parse(XEntry.Text);
                var y = int.Parse(YEntry.Text);
                var width = int.Parse(WidthEntry.Text);
                var height = int.Parse(HeightEntry.Text);
                
                var relativeX = Convert.ToInt16((x / App.Current.MainPage.Height) * 100);
                var relativeY = Convert.ToInt16((y / App.Current.MainPage.Width) * 100);
                var relativeWidth = Convert.ToInt16((width / App.Current.MainPage.Width) * 100);
                var relativeHeight = Convert.ToInt16((height / App.Current.MainPage.Height) * 100);

                Debug.WriteLine($"Relative Values -- X: {relativeX}, Y:{relativeY}, Width:{relativeWidth}, Height: {relativeHeight}");

                var imageBytes = await DependencyService.Get<IRenderService>().RenderRelativeAsync(relativeX, relativeY, relativeWidth, relativeHeight);

                if (imageBytes == null)
                {
                    throw new NullReferenceException("Image was not rendered, the DependencyService returned null. Check the cropping dimensions and try again");
                }

                await Navigation.PushAsync(new ResultsPage(imageBytes) { Title = "Proportional Result" });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"{ex.Message}", "OK");
            }
        }

        private void ReadBarcodeDimensions_OnClicked(object sender, EventArgs e)
        {
            Debug.WriteLine($"Page -- Width:{App.Current.MainPage.Width}, Height: {App.Current.MainPage.Height}");
            Debug.WriteLine($"Barcode Raw Values -- X: {barcode.X}, Y:{barcode.Y}, Width:{barcode.Width}, Height: {barcode.Height}");
            
            XEntry.Text = Convert.ToInt16((barcode.X / App.Current.MainPage.Height) * 100).ToString();
            YEntry.Text = Convert.ToInt16((barcode.Y / App.Current.MainPage.Width) * 100).ToString();
            WidthEntry.Text = Convert.ToInt16((barcode.Width / App.Current.MainPage.Width) * 100).ToString();
            HeightEntry.Text = Convert.ToInt16((barcode.Height / App.Current.MainPage.Height) * 100).ToString();
            
            Debug.WriteLine($"Proportional Values -- X: {XEntry.Text}, Y:{YEntry.Text}, Width:{WidthEntry.Text}, Height: {HeightEntry.Text}");
        }
    }
}
    
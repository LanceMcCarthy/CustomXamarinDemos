using System;
using System.IO;
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

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            PreProcessing();

            var imageBytes = await DependencyService.Get<IRenderService>().RenderAsync();

            PostProcessing(imageBytes);
        }

        private async void Button2_OnClicked(object sender, EventArgs e)
        {
            PreProcessing();

            try
            {
                var x = int.Parse(XEntry.Text);
                var y = int.Parse(YEntry.Text);
                var width = int.Parse(WidthEntry.Text);
                var height = int.Parse(HeightEntry.Text);


                var imageBytes = await DependencyService.Get<IRenderService>().RenderAsync(x, y, width, height);

                PostProcessing(imageBytes);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "One of the crop region values is invalid", "okay");
            }
            
        }

        private void PreProcessing()
        {
            TitleLabel.Text = $"Timestamp: {DateTime.Now:h:mm:ss tt}";
            ResultGrid.Children.Clear();
            ResultGrid.BackgroundColor = Color.Transparent;
        }

        private void PostProcessing(byte[] imageBytes)
        {
            var image = new Image();
            image.Source = ImageSource.FromStream((() => new MemoryStream(imageBytes)));

            ResultGrid.BackgroundColor = Color.Black;
            ResultGrid.Children.Add(image);

            TitleLabel.Text = $"Done!";
        }
    }
}
    
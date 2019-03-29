using System;
using System.IO;
using Xamarin.Forms;

namespace RenderImage.Portable
{
    public partial class ResultsPage : ContentPage
    {
        private byte[] _imageBytes;
        
        public ResultsPage (byte[] imageBytes)
		{
			InitializeComponent();

            _imageBytes = imageBytes;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                BusyIndicator.IsBusy = true;

                image.Source = ImageSource.FromStream(() => new MemoryStream(_imageBytes));

                _imageBytes = null;
            }
            catch(Exception ex)
            {
                ResultsLabel.Text = $"Error: {ex.Message}";
            }
            finally
            {
                BusyIndicator.IsBusy = false;
            }
        }
    }
}
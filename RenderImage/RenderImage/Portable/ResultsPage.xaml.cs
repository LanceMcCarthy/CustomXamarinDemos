using System;
using System.IO;
using System.Threading.Tasks;
using RenderImage.Portable.Models;
using RenderImage.Portable.Services;
using Xamarin.Forms;

namespace RenderImage.Portable
{
    public partial class ResultsPage : ContentPage
    {
        private readonly byte[] _imageBytes;
        
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
                BusyIndicator.IsVisible = BusyIndicator.IsBusy = true;

                image.Source = ImageSource.FromStream(() => new MemoryStream(_imageBytes));
            }
            catch(Exception ex)
            {
                ErrorLabel.IsVisible = true;
                ErrorLabel.Text = $"Error: {ex.Message}";
            }
            finally
            {
                BusyIndicator.IsVisible = BusyIndicator.IsBusy = false;
            }
        }

        private async void MakePdfMenuItem_OnClicked(object sender, EventArgs e)
        {
            await GeneratePdfWithImageAsync();
        }

        private async Task GeneratePdfWithImageAsync()
        {
            try
            {
                BusyIndicator.IsVisible = BusyIndicator.IsBusy = true;

                // convert the byte array to a base64 string (you could use a Stream if you prefer to)
                var base64String = Convert.ToBase64String(_imageBytes);

                var contentForPdf = new MyPdfContent
                {
                    ImageBase64 = base64String,
                    Title = "Example: Xamarin XAML -> PDF",
                    Body = "This document is an example of rendering a XAML UI in a PDF document using Telerik Document Processing Libraries.",
                    BackgroundColor = "#FFFFFF",
                };

                // Generate a PDF document in the cloud using a Web API and Telerik Document Processing libraries. See this example for guidance https://github.com/LanceMcCarthy/TseExamples/blob/master/UploadingToWebApi/UploadingToWebApi.Web/Controllers/PdfGeneratorController.cs
                var pdfBytes = await WebApiService.Instance.GenerateDocumentAsync(contentForPdf);
                
                if (pdfBytes != null)
                {
                    // Navigate to a new page and the PDF in the RadPdfViewer
                    await Navigation.PushAsync(new DocumentViewerPage(pdfBytes));
                }
                else
                {
                    ErrorLabel.IsVisible = true;
                    ErrorLabel.Text = "Error Uploading Content. Try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorLabel.IsVisible = true;
                ErrorLabel.Text = $"Error: {ex.Message}";
            }
            finally
            {
                BusyIndicator.IsVisible = BusyIndicator.IsBusy = false;
            }
        }
    }
}
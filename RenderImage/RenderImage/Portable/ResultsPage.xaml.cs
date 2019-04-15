using System;
using System.IO;
using System.Threading.Tasks;
using RenderImage.Portable.Models;
using RenderImage.Portable.Services;
using SixLabors.ImageSharp;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Fixed.Model;
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

                image.Source = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(_imageBytes));
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

        private async void LocalGenerationButton_OnClicked(object sender, EventArgs e)
        {
            await LocallyGeneratePdfWithImageAsync();
        }

        private async void RemoteGenerationButton_OnClicked(object sender, EventArgs e)
        {
            await RemotelyGeneratePdfWithImageAsync();
        }

        private async Task LocallyGeneratePdfWithImageAsync()
        {
            try
            {
                BusyIndicator.IsVisible = BusyIndicator.IsBusy = true;

                using (var jpegStream = new MemoryStream())
                {
                    SixLabors.ImageSharp.Formats.IImageFormat format;

                    // Converting the png byte array to a jpeg-encoded byte array
                    using (var image = SixLabors.ImageSharp.Image.Load(_imageBytes))
                    {
                        // convert to jpeg
                        image.SaveAsJpeg(jpegStream);

                        var jpegArray = GetByteArray(jpegStream);

                        var document = new RadFixedDocument();
                        var page = new RadFixedPage();

                        var fixedPageImageSource = new Telerik.Windows.Documents.Fixed.Model.Resources.ImageSource(jpegStream);

                        var pdfImage = new Telerik.Windows.Documents.Fixed.Model.Objects.Image();
                        pdfImage.ImageSource = fixedPageImageSource;

                        page.Content.Add(pdfImage);


                        var exportSettings = new PdfExportSettings();
                        exportSettings.ImageQuality = ImageQuality.Medium;


                        PdfFormatProvider provider = new PdfFormatProvider();

                        using (Stream output = File.OpenWrite("sample.pdf"))
                        {
                            provider.Export(document, output);

                            if (output.CanSeek)
                            {
                                output.Seek(0, SeekOrigin.Begin);
                            }
                            else
                            {
                                output.Position = 0;
                            }

                            var pdfBytes = GetByteArray(output);

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
                    }
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

        private async Task RemotelyGeneratePdfWithImageAsync()
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

        public static byte[] GetByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];

            using (var ms = new MemoryStream())
            {
                int read;

                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }
    }
}
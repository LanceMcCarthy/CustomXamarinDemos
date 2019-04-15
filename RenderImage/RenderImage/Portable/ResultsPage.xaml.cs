using System;
using System.IO;
using System.Threading.Tasks;
using CommonHelpers.Extensions;
using RenderImage.Portable.Models;
using RenderImage.Portable.Services;
using Telerik.Documents.Primitives;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
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
                MessagePopup.IsOpen = true;
                ErrorLabel.Text = $"Error: {ex.Message}";
            }
            finally
            {
                BusyIndicator.IsVisible = BusyIndicator.IsBusy = false;
            }
        }

        private async void LocalGenerationButton_OnClicked(object sender, EventArgs e)
        {
            BusyIndicator.IsVisible = BusyIndicator.IsBusy = true;

            await LocallyGeneratePdfWithImageAsync();

            BusyIndicator.IsVisible = BusyIndicator.IsBusy = false;
        }

        private async void RemoteGenerationButton_OnClicked(object sender, EventArgs e)
        {
            BusyIndicator.IsVisible = BusyIndicator.IsBusy = true;

            await RemotelyGeneratePdfWithImageAsync();

            BusyIndicator.IsVisible = BusyIndicator.IsBusy = false;
        }

        /// <summary>
        /// Generates the PDF document locally and saves it to a file.
        /// </summary>
        /// <returns></returns>
        private async Task LocallyGeneratePdfWithImageAsync()
        {
            try
            {
                using (var jpegStream = new MemoryStream(_imageBytes))
                {
                    // Define page dimensions
                    var pageSize = new Telerik.Documents.Primitives.Size(Telerik.Windows.Documents.Media.Unit.MmToDip(210), Telerik.Windows.Documents.Media.Unit.MmToDip(297));
                    var pageMargins = new Telerik.Documents.Primitives.Thickness(Telerik.Windows.Documents.Media.Unit.MmToDip(10));
                    var remainingPageSize = new Telerik.Documents.Primitives.Size(pageSize.Width - pageMargins.Left - pageMargins.Right, pageSize.Height - pageMargins.Top - pageMargins.Bottom);

                    // Create in memory document
                    // instantiate the document and add a page
                    var document = new RadFixedDocument();
                    var page = document.Pages.AddPage();
                    page.Size = pageSize;

                    // instantiate an editor, this is what writes all the content to the page
                    var editor = new FixedContentEditor(page);
                    editor.GraphicProperties.StrokeThickness = 0;
                    editor.GraphicProperties.IsStroked = false;
                    editor.GraphicProperties.FillColor = new RgbColor(255, 255, 255);
                    editor.DrawRectangle(new Rect(0, 0, pageSize.Width, pageSize.Height));
                    editor.Position.Translate(pageMargins.Left, pageMargins.Top);

                    var block = new Block();
                    block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center;
                    block.TextProperties.FontSize = 22;

                    // use the uploaded content for the title
                    block.InsertText("Generated in Xamarin.Forms!");

                    var blockSize = block.Measure(remainingPageSize);
                    editor.DrawBlock(block, remainingPageSize);

                    editor.Position.Translate(pageMargins.Left, blockSize.Height + pageMargins.Top + 20);

                    // Create image that can be inserted into document using the jpeg's stream
                    // !Note - Image is of type Telerik.Windows.Documents.Fixed.Model.Objects.Image
                    var docImageSource = new Telerik.Windows.Documents.Fixed.Model.Resources.ImageSource(jpegStream);

                    // Draw the image into the document
                    var imageBlock = new Block();
                    imageBlock.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center;
                    imageBlock.InsertImage(docImageSource);
                    editor.DrawBlock(imageBlock, remainingPageSize);

                    // Export the document to Pdf
                    var provider = new PdfFormatProvider();

                    using (var exportStream = new MemoryStream())
                    {
                        // Export as PDF file
                        provider.Export(document, exportStream);

                        // Prepare to show the PDF file in the PDFViewer on the next page
                        exportStream.Position = 0;
                        var pdfBytes = GetByteArray(exportStream);

                        if (pdfBytes != null)
                        {
                            // Navigate to a new page and the PDF in the RadPdfViewer
                            await Navigation.PushAsync(new DocumentViewerPage(pdfBytes));
                        }
                        else
                        {
                            MessagePopup.IsOpen = true;
                            ErrorLabel.Text = "The exported byte array was null. Try again.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessagePopup.IsOpen = true;
                ErrorLabel.Text = $"Error: {ex.Message}";
            }
        }

        /// <summary>
        /// Creates the PDF document on the server and returns the PDF file as a byte array
        /// </summary>
        /// <returns></returns>
        private async Task RemotelyGeneratePdfWithImageAsync()
        {
            try
            {
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
                    await pdfBytes.SaveToLocalFolderAsync("RemoteGenerated.pdf");

                    // Navigate to a new page and the PDF in the RadPdfViewer
                    await Navigation.PushAsync(new DocumentViewerPage(pdfBytes));
                }
                else
                {
                    MessagePopup.IsOpen = true;
                    ErrorLabel.Text = "Error Uploading Content. Try again.";
                }
            }
            catch (Exception ex)
            {
                MessagePopup.IsOpen = true;
                ErrorLabel.Text = $"Error: {ex.Message}";
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

        private void ClosePopupButton_OnClicked(object sender, EventArgs e)
        {
            MessagePopup.IsOpen = false;
        }
    }
}
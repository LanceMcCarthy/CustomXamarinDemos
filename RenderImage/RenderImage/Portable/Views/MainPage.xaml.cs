using System;
using System.IO;
using System.Threading.Tasks;
using CommonHelpers.Extensions;
using RenderImage.Portable.Controls;
using RenderImage.Portable.Models;
using RenderImage.Portable.Services;
using Telerik.Documents.Primitives;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Xamarin.Forms;

namespace RenderImage.Portable.Views
{
    public partial class MainPage : DuoPage
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
                BoardingPassBarcode.Value = "Mrs Lara Howard SOF LIS S7 129 12C 06 June 2018 6:30 pm 12 36";
                //await imageBytes.SaveToLocalFolderAsync(App.CapturedImageFilePath);
                //ResultImage.Source = ImageSource.FromStream((() => new MemoryStream(imageBytes)));
            }
        }

        private async void ExportButton_OnClicked(object sender, EventArgs e)
        {
            BusyIndicator.IsVisible = BusyIndicator.IsBusy = true;

            var success = await Task.Run(async () =>
            {
                try
                {
                    var imageBytes = await FileExtensions.LoadFileBytesAsync(App.EditedImageFilePath);

                    using (var jpegStream = new MemoryStream(imageBytes))
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

                        var block = new Block
                        {
                            HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center
                        };
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
                        var imageBlock = new Block
                        {
                            HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center
                        };
                        imageBlock.InsertImage(docImageSource);
                        editor.DrawBlock(imageBlock, remainingPageSize);

                        // Export the document to Pdf
                        var provider = new PdfFormatProvider();

                        if (File.Exists(App.PdfFilePath))
                        {
                            File.Delete(App.PdfFilePath);
                        }

                        using (var fileStream = File.OpenWrite(App.PdfFilePath))
                        {
                            provider.Export(document, fileStream);
                        }

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() => DisplayAlert("Error", ex.Message, "ok"));

                    return false;
                }
            });

            BusyIndicator.IsVisible = BusyIndicator.IsBusy = false;

            if (success)
            {
                await Navigation.PushAsync(new DocumentViewerPage());
            }
        }

        private void GeneratePassButton_OnClicked(object sender, EventArgs e)
        {
            BoardingPassBorder.IsVisible = true;
            BoardingPassBarcode.IsVisible = true;
            BoardingPassBarcode.Value = "Mrs Lara Howard SOF LIS S7 129 12C 06 June 2018 6:30 pm 12 36";
        }
    }
}

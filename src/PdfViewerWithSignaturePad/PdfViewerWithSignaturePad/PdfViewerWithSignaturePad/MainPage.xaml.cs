using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Filters;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.Annotations;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Resources;
using Telerik.XamarinForms.Input;
using Telerik.XamarinForms.PdfViewer;
using Xamarin.Forms;

namespace PdfViewerWithSignaturePad
{
    public partial class MainPage : ContentPage
    {
        private RadFixedDocument originalDocument;

        public MainPage()
        {
            InitializeComponent();
        }
        
        private void LoadButton_OnClicked(object sender, EventArgs e)
        {
            originalDocument = HelperMethods.ImportPdfFileToFixedDocument("Original.pdf");

            PdfViewer1.Source = new FixedDocumentSource(originalDocument);
        }

        private async void SignAndSaveButton_OnClicked(object sender, EventArgs e)
        {
            var imgBytes = await HelperMethods.GetSignatureImageAsync(SignaturePad1, ImageFormat.Jpeg);
            
            var signedDocument = AddSignatureToDocument(originalDocument, imgBytes, ImageFormat.Jpeg);
            
            if (signedDocument != null)
            {
                Debug.WriteLine($"Saving signed document as PDF file...", "SignAndSave");

                // 1.Export the completed FixedDocument to a PDF
                byte[] pdfFileBytes = new PdfFormatProvider().Export(signedDocument);

                // 2. Save the PDF as a file
                var localfolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var fileName = "OriginalDocument_SIGNED.pdf";
                var filePath = Path.Combine(localfolder, fileName);

                File.WriteAllBytes(filePath, pdfFileBytes);

                Debug.WriteLine($"Showing signed document in PdfViewer", "SignAndSave");

                // 3. as a final step, you can show the signed PDF in the PdfViewer
                PdfViewer1.Source = null;
                PdfViewer1.Source = new FixedDocumentSource(signedDocument);
            }
            else
            {
                Debug.WriteLine($"SignedDocument was null", "SignAndSave");

                await DisplayAlert("No Signature Field", "There was no signature field detected in the document, signing incomplete.", "OK");
            }
        }

        private static RadFixedDocument AddSignatureToDocument(RadFixedDocument document, byte[] imageBytes, Telerik.XamarinForms.Input.ImageFormat imgFormat)
        {
            Debug.WriteLine($"AddSignatureToDocument Started, using {imgFormat}.", "Document Processing");

            Widget signatureWidget = null;
            
            // We'll use a clone of the original document to perform editing operations with (just in case something goes wrong)
            Debug.WriteLine($"Cloning original document...", "Document Processing");

            RadFixedDocument workingDocument = document.Clone();

            Debug.WriteLine($"Document cloned, iterating over FormFields...", "Document Processing");

            var formFieldsCount = workingDocument.AcroForm.FormFields.Count;

            Debug.WriteLine($"Found {formFieldsCount} FormFields!", "Document Processing");

            foreach (var formField in workingDocument.AcroForm.FormFields)
            {
                var widgetsCount = formField.Widgets.Count();

                Debug.WriteLine($"Found {widgetsCount} Widgets in the {formField.Name} FormField", "Document Processing");

                foreach (var widget in formField.Widgets)
                {
                    Debug.WriteLine($"Found a {widget.WidgetContentType} in {widget.Field.Name} widget", "Document Processing");

                    // We've discovered a SignatureWidget in the document.

                    if (widget.WidgetContentType == WidgetContentType.SignatureContent)
                    {
                        // We've discovered a SignatureWidget in the document.
                        Debug.WriteLine($"***** Discovered a SignatureWidget! *****", "Document Processing");
                        Debug.WriteLine($"Widget Located at: X: {widget.Rect.X}, Y: {widget.Rect.Y}", "Document Processing");

                        // IMPORTANT: We're creating another references to this widget to differentiate it and operate on
                        signatureWidget = widget;
                        
                        // WARNING - this demo has the signature on the first page, if yours is on a different page, you'll need to update this
                        var currentPage = workingDocument.Pages.First();

                        // An easy way to edit the document is to use a FixedContentEditor. See https://docs.telerik.com/devtools/document-processing/libraries/radpdfprocessing/editing/fixedcontenteditor

                        Debug.WriteLine($"Starting up a FixedContentEditor at the first page...", "Document Processing");

                        FixedContentEditor editor = new FixedContentEditor(currentPage);

                        Debug.WriteLine($"Editor Created, starting position at {editor.Position.Matrix.OffsetX}, {editor.Position.Matrix.OffsetY}", "Document Processing");

                        // Move the Editor to the same location on the page that the SignatureWidget was
                        // See https://docs.telerik.com/devtools/document-processing/libraries/radpdfprocessing/concepts/position
                        editor.Position.Translate(signatureWidget.Rect.X, signatureWidget.Rect.Y);

                        Debug.WriteLine($"Moved Editor to position - Matrix.OffsetX: {editor.Position.Matrix.OffsetX}, Matrix.OffsetY: {editor.Position.Matrix.OffsetY}", "Document Processing");

                        Debug.WriteLine($"Loading JPEG byte[] into MemoryStream...", "Document Processing");

                        // Draw the image on the document
                        // See https://docs.telerik.com/devtools/document-processing/libraries/radpdfprocessing/editing/fixedcontenteditor#inserting-image
                        var imageHeight = Convert.ToInt32(signatureWidget.Rect.Height);
                        var imageWidth = Convert.ToInt32(signatureWidget.Rect.Width);


                        Debug.WriteLine($"Processing {imgFormat} image - Width: {imageWidth}, Height: {imageHeight}", "Document Processing");

                        Telerik.Windows.Documents.Fixed.Model.Resources.ImageSource imageSource = null;

                        // Check what the image format is, we use different paths depending on PNG or JPEG to create ImageSource
                        if (imgFormat == ImageFormat.Png)
                        {
                            // Because we're in a .NET Standard 2.0 project type, we need to manually handle the pixels in the byte[] for transparency
                            HelperMethods.GetRawDataFromRgbaSource(imageBytes, out var data, out var alpha);

                            byte[] rawAlpha = HelperMethods.CompressDataWithDeflate(alpha);

                            EncodedImageData imageData = new EncodedImageData(
                                imageBytes,
                                rawAlpha,
                                8,
                                imageWidth,
                                imageHeight,
                                ColorSpaceNames.DeviceRgb,
                                new string[] { PdfFilterNames.FlateDecode });
                            
                            imageSource = new Telerik.Windows.Documents.Fixed.Model.Resources.ImageSource(imageData);
                        }
                        else if (imgFormat == ImageFormat.Jpeg)
                        {
                            using (var imgStream = new MemoryStream(imageBytes))
                            {
                                // For JPEG, use the extensibility manager to convert the image data to jpeg
                                FixedExtensibilityManager.JpegImageConverter.TryConvertToJpegImageData(imageBytes, ImageQuality.Low, out var jpegImageData);

                                imageSource = new Telerik.Windows.Documents.Fixed.Model.Resources.ImageSource(new MemoryStream(jpegImageData));
                            }
                        }

                        Debug.WriteLine($"Drawing {imgFormat} image - Width: {imageWidth}, Height: {imageHeight}", "Document Processing");

                        editor.DrawImage(imageSource, imageWidth, imageHeight);
                        
                        Debug.WriteLine($"Draw complete", "Document Processing");
                    }
                }
            }
            
            if (signatureWidget != null)
            {
                // Finally, remove the SignatureWidget
                Debug.WriteLine($"Draw complete. Attempting to remove SignatureWidget with the field name of {signatureWidget.Field.Name}...", "Document Processing");

                if (workingDocument.AcroForm.FormFields.Contains(signatureWidget.Field.Name))
                {
                    Debug.WriteLine($"Found FormField {signatureWidget.Field.Name}, removing...", "Document Processing");

                    workingDocument.AcroForm.FormFields.Remove(signatureWidget.Field);

                    Debug.WriteLine($"Successfully removed FormField.", "Document Processing");
                }
                else
                {
                    Debug.WriteLine($"WARNING: FormField {signatureWidget.Field.Name} not found! Removal unsuccessful..", "Document Processing");
                }

                return workingDocument;
            }

            // If the signatureField wasn't found, we return null, which cancels the operation.
            return null;
        }
    }
}

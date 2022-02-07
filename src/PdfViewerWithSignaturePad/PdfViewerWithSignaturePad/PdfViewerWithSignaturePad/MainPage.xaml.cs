using System;
using System.Diagnostics;
using System.IO;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            originalDocument = HelperMethods.ImportPdfFileToFixedDocument("Original.pdf");

            PdfViewer1.Source = new FixedDocumentSource(originalDocument);
        }

        private async void SignAndSaveButton_OnClicked(object sender, EventArgs e)
        {
            var localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            
            // Option 1 - using filepath for signature image source
            var signatureImageFilePath = Path.Combine(localFolder, "Signature.jpg");
            await HelperMethods.SaveSignatureImageAsync(SignaturePad1, ImageFormat.Jpeg, signatureImageFilePath);
            var signedDocument = HelperMethods.AddSignatureToDocument(originalDocument, signatureImageFilePath, ImageFormat.Jpeg);

            // Option 2 - using byte[] for signature image source
            // var imgBytes = await HelperMethods.GetSignatureImageAsync(SignaturePad1, ImageFormat.Jpeg);
            // var signedDocument = HelperMethods.AddSignatureToDocument(originalDocument, imgBytes, ImageFormat.Jpeg);
            
            // Save the updated document
            if (signedDocument != null)
            {
                // 1.Export the completed FixedDocument to a PDF
                byte[] exportedPdfFileData = new PdfFormatProvider().Export(signedDocument);

                // 2. Save the PDF as a file
                var updatedPdfFilePath = Path.Combine(localFolder, "OriginalDocument_SIGNED.pdf");
                File.WriteAllBytes(updatedPdfFilePath, exportedPdfFileData);
                
                // 3. as a final step, you can show the signed PDF in the PdfViewer
                PdfViewer1.Source = null;
                PdfViewer1.Source = new FileDocumentSource(updatedPdfFilePath);

                await DisplayAlert("Signing Complete", $"The document has been saved to {updatedPdfFilePath}", "OK");
            }
            else
            {
                await DisplayAlert("No Signature Field", "There was no signature field detected in the document, signing incomplete.", "OK");
            }
        }
    }
}

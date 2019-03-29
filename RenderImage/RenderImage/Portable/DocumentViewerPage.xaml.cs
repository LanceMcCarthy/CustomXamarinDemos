using Telerik.XamarinForms.PdfViewer;
using Xamarin.Forms;

namespace RenderImage.Portable
{
    public partial class DocumentViewerPage : ContentPage
    {
        private readonly byte[] _documentBytes;

		public DocumentViewerPage(byte[] documentBytes)
		{
			InitializeComponent();

            // holding the document in a field so that you can save it to a file later if you want to
            _documentBytes = documentBytes;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            // Load the PDF into the PdfViewer using the ByteArrayDocumentSource, there are many other options to use.
            Viewer.Source = new ByteArrayDocumentSource(_documentBytes);
        }
    }
}
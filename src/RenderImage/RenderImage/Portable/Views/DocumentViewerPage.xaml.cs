using Telerik.XamarinForms.PdfViewer;
using Xamarin.Forms;

namespace RenderImage.Portable.Views
{
    public partial class DocumentViewerPage : ContentPage
    {
		public DocumentViewerPage()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            Viewer.Source = new FileDocumentSource(App.PdfFilePath);

            base.OnAppearing();
        }
    }
}
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Telerik.XamarinForms.RichTextEditor;
using Xamarin.Forms;

namespace RteImages.Portable
{
    public partial class MainPage : ContentPage
    {
        private RichTextHtmlStringSource htmlSource;

        public MainPage()
        {
            InitializeComponent();

            htmlSource = new RichTextHtmlStringSource
            {
                Html = @"<h4>One of the Most Beautiful Islands on Earth - Tenerife</h4>
            <p><strong>Tenerife</strong> is the largest and most populated island of the eight <a href='https://en.wikipedia.org/wiki/Canary_Islands' target='_blank'>Canary Islands</a>.</p>
            <p style='color:#808080'>It is also the most populated island of <strong>Spain</strong>, with a land area of <i>2,034.38 square kilometers</i> and <i>904,713</i> inhabitants, 43% of the total population of the <strong>Canary Islands</strong>.</p>"
            };

            this.richTextEditor.Source = htmlSource;
        }

        private async Task InsertImageIntoEditor(string imgHtml)
        {
            if (string.IsNullOrEmpty(imgHtml))
            {
                throw new ArgumentNullException(nameof(imgHtml), "The img element cannot be empty or null.");
            }

            // Option 1 - Try to find exact position in document and insert there
            // Get selection 
            //RichTextSelection currentSelection = await richTextEditor.GetSelectionAsync();
            //var insertPosition = currentSelection.Start;

            // Option 2 - Pick a known position and insert it there (beginning/end)
            var insertPosition = htmlSource.Html.Length;

            // Update HTML and the RTE
            var updatedHtml = htmlSource.Html.Insert(insertPosition, imgHtml);

            htmlSource = new RichTextHtmlStringSource { Html = updatedHtml };

            richTextEditor.Source = htmlSource;
        }

        private async void FromUrlToolbarItem_OnTapped(object sender, EventArgs e)
        {
            // **** Phase 1 **** //

            var imgHtml = "";
            var url = "https://docs.telerik.com/devtools/xamarin/front-image.png";

            using (var client = new HttpClient())
            {
                var imgBytes = await client.GetByteArrayAsync(url);

                var base64EncodedString = Convert.ToBase64String(imgBytes);

                var imageFormat = "png";
                imgHtml = $"<img src='data:image/{imageFormat};base64,{base64EncodedString}'/>";
            }

            // **** Phase 2 **** //

            await InsertImageIntoEditor(imgHtml);
        }

        private async void FileStreamToolbarItem_OnTapped(object sender, EventArgs e)
        {
            // **** Phase 1 **** //
            var imgHtml = "";
            var filePath = "RteImages.Portable.Images.ninja.png";

            using (var stream = typeof(MainPage).GetTypeInfo().Assembly.GetManifestResourceStream(filePath))
            {
                var length = stream.Length;
                var imgBytes = new byte[length];

                await stream.ReadAsync(imgBytes, 0, (int)length);

                var base64EncodedString = Convert.ToBase64String(imgBytes);
                var imageFormat = "png";

                imgHtml = $"<img src='data:image/{imageFormat};base64,{base64EncodedString}'/>";
            }

            // **** Phase 2 **** //

            await InsertImageIntoEditor(imgHtml);

        }

        private async void StaticBase64ToolbarItem_OnTapped(object sender, EventArgs e)
        {
            // **** Phase 1 **** //

            var imgSrcData = Helpers.GetTestImage();
            var imgHtml = $"<img src='{imgSrcData}'/>";

            // **** Phase 2 **** //

            await InsertImageIntoEditor(imgHtml);
        }
    }
}

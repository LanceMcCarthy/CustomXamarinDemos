using System;
using System.Threading.Tasks;
using SignaturePad.Forms;
using Telerik.XamarinForms.RichTextEditor;
using Xamarin.Forms;

namespace SignaturePanel.Portable
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            LoadDefaultDocument();
        }

        private async void SaveButton_OnClicked(object sender, EventArgs e)
        {
            await SaveAndInsertSignatureAsync();
        }

        private void ClearButton_OnClicked(object sender, EventArgs e)
        {
            SignatureView.Clear();
            LoadDefaultDocument();
        }

        private async Task SaveAndInsertSignatureAsync()
        {
            using (var imgStream = await SignatureView.GetImageStreamAsync(
                       format: SignatureImageFormat.Png, 
                       settings:new ImageConstructionSettings
                       {
                           BackgroundColor = Color.Transparent,
                           StrokeColor = Color.Black,
                           ShouldCrop = true
                       }))
            {
                var img = new RichTextImage(
                    source: RichTextImageSource.FromStream(imgStream, RichTextImageType.Png),
                    title: "customer signature",
                    width:150,
                    height:65);

                RichTextEditor1.InsertImageCommand.Execute(img);
            }
        }
        
        private void LoadDefaultDocument()
        {
            var htmlSource = @"<h4>Product Agreement</h4><p>Please sign below and click save to agree to the terms of use. You can find all the Progress Telerik license agreements <a href='https://www.telerik.com/purchase/license-agreements' target='_blank'>here</a></p><p>Customer Signature:</p></p>";
            RichTextEditor1.Source = RichTextSource.FromString(htmlSource);
        }
    }
}

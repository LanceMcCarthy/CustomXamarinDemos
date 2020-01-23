using System;
using System.IO;
using Telerik.XamarinForms.ImageEditor;
using Xamarin.Forms;

namespace RenderImage.Portable.Views
{
    public partial class ImgEditorPage : ContentPage
    {
        public ImgEditorPage()
        {
            InitializeComponent();
        }

        private async void SaveButton_OnClicked(object sender, EventArgs e)
        {
            if (File.Exists(App.EditedImageFilePath))
            {
                File.Delete(App.EditedImageFilePath);
            }

            using (var fileStream = File.Create(App.EditedImageFilePath))
            {
                await imageEditor.SaveAsync(fileStream, ImageFormat.Jpeg, 0.9);
            }

            //await Navigation.PushAsync(new ResultsView());
        }
    }
}
using System;
using System.IO;
using GhostFile.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GhostFile.Portable
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void UploadButton_OnClick(object sender, EventArgs e)
        {
            using (var stream = await FileSystem.OpenAppPackageFileAsync("GeometricFlower_NormalMap.jpg"))
            using (var ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);
                ms.Position = 0;

                var fileBytes = ms.ToArray();

                var result = await FileIoApiService.Instance.UploadFileAsync(fileBytes, "1");

                if (result != null)
                {
                    if(result.Success)
                    {
                        KeyOutput.Text = result.Key;
                        LinkOutput.Text = result.Link;
                        ExpiryOutput.Text = result.Expiry;
                    }
                    else
                    {
                        ErrorOutput.Text = result.Error;
                        MessageOutput.Text = result.Message;
                    }
                }
            }
        }
    }
}

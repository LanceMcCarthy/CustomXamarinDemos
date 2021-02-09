using System;
using System.IO;
using RenderImage.Portable.Views;
using Xamarin.Forms;

namespace RenderImage.Portable
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public static string CapturedImageFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "capture_img.bin");

        public static string EditedImageFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "edited_img.bin");

        public static string PdfFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Exported.pdf");
    }
}

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

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }

    public static class ServiceConstants
    {
        public const string ServiceBaseUrl = "http://webapifortelerikdemos.azurewebsites.net/";
        public const string PdfGeneratorApi = "api/pdfgenerator";
    }
}

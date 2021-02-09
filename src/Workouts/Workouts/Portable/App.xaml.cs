using Workouts.Portable.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly:XamlCompilation(XamlCompilationOptions.Compile)]
namespace Workouts.Portable
{
    public partial class App : Application
    {
        public static DataService ApiService { get; set; }

        public App()
        {
            InitializeComponent();

            ApiService = new DataService();

            MainPage = new MainPage();
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
}

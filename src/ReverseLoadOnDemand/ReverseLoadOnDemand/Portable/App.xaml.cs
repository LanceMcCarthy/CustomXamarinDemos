using Xamarin.Forms;

namespace ReverseLoadOnDemand.Portable
{
    public partial class App : Application
    {
        public static ItemDataService DataService { get; set; }

        public App()
        {
            InitializeComponent();

            // Mocking a DependencyInjection service
            App.DataService = new ItemDataService();

            MainPage = new MainPage();
        }
    }
}

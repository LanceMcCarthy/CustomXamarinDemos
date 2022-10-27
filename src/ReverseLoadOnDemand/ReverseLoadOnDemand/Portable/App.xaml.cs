using ReverseLoadOnDemand.Portable.Services;
using Xamarin.Forms;

namespace ReverseLoadOnDemand.Portable;

public partial class App : Application
{
    public static ItemDataService DataService { get; set; }
    public static ChatDataService ChatService { get; set; }

    public App()
    {
        InitializeComponent();

        // Mocking a DependencyInjection service
        App.DataService = new ItemDataService();
        App.ChatService = new ChatDataService();

        MainPage = new RootPage();
    }
}

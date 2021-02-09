using CallDetector.Portable.Common;
using CallDetector.Portable.Models;
using CallDetector.Portable.ViewModels;
using CallDetector.Portable.Views;
using Xamarin.Forms;

namespace CallDetector.Portable
{
    public partial class MainPage : ContentPage, INavigationHandler
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel { NavigationHandler = this };
            SideDrawer.MainContent = new MainView();
        }
        
        public void LoadView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Main:
                    SideDrawer.MainContent = new MainView();
                    break;
                case ViewType.CallLog:
                    SideDrawer.MainContent = new CallLogView();
                    break;
                case ViewType.About:
                    SideDrawer.MainContent = new AboutView();
                    break;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (SideDrawer.MainContent.GetType() == typeof(CallLogView) || SideDrawer.MainContent.GetType() == typeof(AboutView))
            {
                SideDrawer.MainContent = new MainView();
                return true;
            }

            return base.OnBackButtonPressed();
        }
    }
}

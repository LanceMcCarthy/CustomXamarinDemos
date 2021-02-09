using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Update: Added XamlC to prevent iOS Linker from stripping out referenced assembly code
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DrawerDismissEffect.Portable
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

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

using Workouts.Portable.Common;
using Workouts.Portable.ViewModels;
using Workouts.Portable.Views;
using Xamarin.Forms;

namespace Workouts.Portable
{
    public partial class MainPage : ContentPage, INavigationHandler
    {
        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = new ViewModel { NavigationHandler = this };

            LoadView(ViewType.Home);
        }

        public void LoadView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    this.Content = new HomeView();
                    break;
                case ViewType.Details:
                    this.Content = new DetailsView();
                    break;
                default:

                    break;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await (BindingContext as ViewModel).LoadExercisesAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            var viewType = this.Content.GetType();

            if (viewType == typeof(DetailsView))
            {
                this.Content = new HomeView();
                return true;
            }

            return base.OnBackButtonPressed();
        }
    }
}

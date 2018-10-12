using System.Linq;
using Telerik.XamarinForms.DataGrid;
using TelerikThemeEditor.Portable.Models;
using Xamarin.Forms;

namespace TelerikThemeEditor.Portable.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(!ViewModel.ThemeColors.Any())
            {
                ViewModel.LoadThemeResources();
            }
        }

        private async void DataGrid_OnSelectionChanged(object sender, DataGridSelectionChangedEventArgs e)
        {
            if(e.AddedItems?.Count() > 0)
            {
                var item = e.AddedItems.FirstOrDefault() as ThemeItem;
                await Navigation.PushModalAsync(new EditPage(item));
            }
        }
    }
}

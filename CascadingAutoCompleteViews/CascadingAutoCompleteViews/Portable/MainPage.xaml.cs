using System.Collections.Generic;
using CascadingAutoCompleteViews.Portable.Helpers;
using Telerik.XamarinForms.Input.AutoComplete;
using Xamarin.Forms;

namespace CascadingAutoCompleteViews.Portable
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            CountriesAcv.ItemsSource = new List<string>
            {
                "United States",
                "Canada",
            };
        }
        
        private void CountriesAcv_OnFocused(object sender, FocusEventArgs e)
        {
            CountriesAcv.ShowSuggestions();
        }

        private void StatesAcv_OnFocused(object sender, FocusEventArgs e)
        {
            StatesAcv.ShowSuggestions();
        }

        private void CityAcv_OnFocused(object sender, FocusEventArgs e)
        {
            CityAcv.ShowSuggestions();
        }

        private void CountriesAcv_OnSuggestionItemSelected(object sender, SuggestionItemSelectedEventArgs e)
        {
            var selectedCountry = e.DataItem as string;

            StatesAcv.IsEnabled = true;
            StatesAcv.ItemsSource = Lookups.StatesLookup(selectedCountry);
            StatesAcv.Focus();
        }

        private void StatesAcv_OnSuggestionItemSelected(object sender, SuggestionItemSelectedEventArgs e)
        {
            var selectedState = e.DataItem as string;

            CityAcv.IsEnabled = true;
            CityAcv.ItemsSource = Lookups.CitiesLookup(selectedState);
            CityAcv.Focus();
        }
    }
}

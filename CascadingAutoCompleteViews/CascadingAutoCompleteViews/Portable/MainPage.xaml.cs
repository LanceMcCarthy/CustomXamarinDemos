using System.Collections.Generic;
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
            StatesAcv.ItemsSource = StatesLookup(selectedCountry);
            StatesAcv.Focus();
        }

        private void StatesAcv_OnSuggestionItemSelected(object sender, SuggestionItemSelectedEventArgs e)
        {
            var selectedState = e.DataItem as string;

            CityAcv.IsEnabled = true;
            CityAcv.ItemsSource = CitiesLookup(selectedState);
            CityAcv.Focus();
        }
        
        private List<string> StatesLookup(string country)
        {
            var statesResult = new List<string>();

            if (country == "United States")
            {
                statesResult.Add("Massachusetts");
                statesResult.Add("California");
                statesResult.Add("Texas");
            }

            if (country == "Canada")
            {
                statesResult.Add("Quebec");
                statesResult.Add("Ontario");
                statesResult.Add("Manitoba");
            }

            return statesResult;
        }

        private List<string> CitiesLookup(string state)
        {
            var citiesResult = new List<string>();

            if (state == "Massachusetts")
            {
                citiesResult.Add("Boston");
                citiesResult.Add("Salem");
                citiesResult.Add("Bedford");
            }
            if (state == "California")
            {
                citiesResult.Add("Sacramento");
                citiesResult.Add("San Diego");
            }
            if (state == "Texas")
            {
                citiesResult.Add("Austin");
                citiesResult.Add("Dallas");
            }

            if (state == "Quebec")
            {
                citiesResult.Add("Quebec");
            }
            if (state == "Ontario")
            {
                citiesResult.Add("Toronto");
                citiesResult.Add("Ottawa");
            }
            if (state == "Manitoba")
            {
                citiesResult.Add("Winnipeg");
            }

            return citiesResult;
        }

    }
}

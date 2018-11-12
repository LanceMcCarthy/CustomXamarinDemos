using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CascadingAutoCompleteViews.Portable.Helpers;
using CascadingAutoCompleteViews.Portable.Interfaces;
using CommonHelpers.Common;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace CascadingAutoCompleteViews.Portable.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        // Fields

        private ObservableCollection<string> _states;
        private ObservableCollection<string> _cities;
        private string _selectedCountry;
        private string _selectedState;
        private string _selectedCity;
        private bool _isCountriesAvcEnabled;
        private bool _isStatesAvcEnabled;
        private bool _isCitiesAvcEnabled;

        public ViewModel()
        {
            AutoCompleteFocusedCommand = new Command(ExecuteAutoCompleteFocused);

            Countries = new ObservableCollection<string>
            {
                "United States",
                "Canada"
            };

            IsCountriesAvcEnabled = true;
        }

        // Properties
        
        public IAutoCompleteService AutoCompleteService { get; set; }

        public Command AutoCompleteFocusedCommand { get; set; }

        public ObservableCollection<string> Countries { get; set; }

        public ObservableCollection<string> States
        {
            get => _states;
            set => SetProperty(ref _states, value);
        }

        public ObservableCollection<string> Cities
        {
            get => _cities;
            set => SetProperty(ref _cities, value);
        }
        
        public string SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                if (SetProperty(ref _selectedCountry, value))
                {
                    if (!string.IsNullOrEmpty(_selectedCountry))
                    {
                        // Note: be aware that this is set even if the user types partial values directly into the box instead of s Suggestion selection
                        // This example only checks for a full match in the ItemSource, you may want to take further precaution
                        if (Countries.Contains(_selectedCountry))
                        {
                            IsStatesAvcEnabled = true;
                            States = new ObservableCollection<string>(Lookups.StatesLookup(_selectedCountry));
                            AutoCompleteService.Focus("StatesAcv");
                        }
                    }
                }
            }
        }

        public string SelectedState
        {
            get => _selectedState;
            set
            {
                if (SetProperty(ref _selectedState, value))
                {
                    if (!string.IsNullOrEmpty(_selectedState))
                    {
                        // Note: aware that even if the user types partial values instead of making a selection
                        // This example only checks for a full match in the ItemSource, you may want to take further precaution
                        if (States.Contains(_selectedState))
                        {
                            IsCitiesAvcEnabled = true;
                            Cities = new ObservableCollection<string>(Lookups.CitiesLookup(_selectedState));
                            AutoCompleteService.Focus("CitiesAcv");
                        }
                    }
                }
            }
        }

        public string SelectedCity
        {
            get => _selectedCity;
            set => SetProperty(ref _selectedCity, value);
        }

        public bool IsCountriesAvcEnabled
        {
            get => _isCountriesAvcEnabled;
            set => SetProperty(ref _isCountriesAvcEnabled, value);
        }

        public bool IsStatesAvcEnabled
        {
            get => _isStatesAvcEnabled;
            set => SetProperty(ref _isStatesAvcEnabled, value);
        }

        public bool IsCitiesAvcEnabled
        {
            get => _isCitiesAvcEnabled;
            set => SetProperty(ref _isCitiesAvcEnabled, value);
        }

        // Methods

        private void ExecuteAutoCompleteFocused(object obj)
        {
            var acv = obj as RadAutoCompleteView;
            acv?.ShowSuggestions();
        }
    }
}

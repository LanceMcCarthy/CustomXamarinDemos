using System.Collections.ObjectModel;
using CommonHelpers.Models;
using Telerik.XamarinForms.DataControls.ListView;
using Xamarin.Forms;

namespace RealTimeFilteringDemos.Portable
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            PeopleListView.ItemsSource = new ObservableCollection<Person>
            {
                new Person{ Name = "Freda Curtis"},
                new Person{ Name = "Jeffery Francis"},
                new Person{ Name = "Eva Lawson"},
                new Person{ Name = "Emmett Santos"},
                new Person{ Name = "Theresa Bryan"},
                new Person{ Name = "Jenny Fuller"},
                new Person{ Name = "Terrell Norris"},
                new Person{ Name = "Eric Wheeler"},
                new Person{ Name = "Julius Clayton"},
                new Person{ Name = "Alfredo Thornton"},
                new Person{ Name = "Roberto Romero"},
                new Person{ Name = "Orlando Mathis"},
                new Person{ Name = "Eduardo Thomas"}
            };
        }

        private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //Every time the user enters a new text, clear the existing filters
            PeopleListView.FilterDescriptors.Clear();

            // Create a new descriptor
            PeopleListView.FilterDescriptors.Add(new DelegateFilterDescriptor { Filter = NameFilter });
        }

        private bool NameFilter(object arg)
        {
            if (arg is Person person)
            {
                var name = person.Name.ToLower();
                var searchTerm = SearchBox.Text.ToLower();

                return name.Contains(searchTerm);
            }

            return false;
        }
    }
}

using System;
using Xamarin.Forms;

namespace RangeSelectionTest.Portable
{
    public partial class MainPage : ContentPage
    {
        private int incrementer = 1;

        public MainPage()
        {
            InitializeComponent();
            
            SelectionEffect.StartDate = radCalendar.DisplayDate;
            SelectionEffect.EndDate = radCalendar.DisplayDate;
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            incrementer++;
            
            SelectionEffect.StartDate = radCalendar.DisplayDate;
            SelectionEffect.EndDate = radCalendar.DisplayDate.AddDays(incrementer);
        }
    }
}

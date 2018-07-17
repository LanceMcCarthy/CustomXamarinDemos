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

            SelectionEffect.StartDate = DateTime.Today.AddDays(-3);
            SelectionEffect.EndDate = DateTime.Today;
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            incrementer++;

            SelectionEffect.StartDate = DateTime.Today.AddDays(incrementer - 3);
            SelectionEffect.EndDate = DateTime.Today.AddDays(incrementer);

        }
    }
}

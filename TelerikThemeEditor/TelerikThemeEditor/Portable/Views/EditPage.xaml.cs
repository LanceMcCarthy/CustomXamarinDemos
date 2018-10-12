using System;
using TelerikThemeEditor.Portable.Models;
using Xamarin.Forms;

namespace TelerikThemeEditor.Portable.Views
{
    
	public partial class EditPage : ContentPage
	{
        //never used, kept for designer stability
		public EditPage() {}

	    public EditPage(ThemeItem item)
	    {
	        InitializeComponent();
	        BindingContext = item;
	    }

        private async void SaveButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

	    private async void CancelButton_OnClicked(object sender, EventArgs e)
	    {
	        (BindingContext as ThemeItem).SelectedThemeColor = (BindingContext as ThemeItem).OriginalThemeColor;

            await Navigation.PopModalAsync();
	    }
    }
}
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CascadingAutoCompleteViews.Portable.Interfaces;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace CascadingAutoCompleteViews.Portable
{
    public partial class MvvmPage : ContentPage, IAutoCompleteService
    {
	    public MvvmPage()
	    {
		    InitializeComponent();
		    viewModel.AutoCompleteService = this;
	    }
        
        public void Focus(string controlName)
        {
            this.FindByName<RadAutoCompleteView>(controlName)?.Focus();
        }
    }
}
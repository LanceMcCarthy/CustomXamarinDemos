using System;
using Xamarin.Forms;

namespace Workouts.Portable.Views
{
	public partial class DetailsView : ContentView
	{
		public DetailsView ()
		{
			InitializeComponent ();

            this.BindingContextChanged += DetailsView_BindingContextChanged;
		}

        private void DetailsView_BindingContextChanged(object sender, EventArgs e)
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(1500), () =>
            {
                TensionImage.IsVisible = !TensionImage.IsVisible;
                RelaxationImage.IsVisible = !RelaxationImage.IsVisible;

                return true;
            });
        }
    }
}
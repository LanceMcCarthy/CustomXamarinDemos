using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsyncTemplateCellDemo.Portable
{
    public class MyListViewTemplateCell : Telerik.XamarinForms.DataControls.ListView.ListViewTemplateCell
    {
        protected override async void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (this.BindingContext == null)
            {
                return;
            }

            // When the BindingContext is not null, this means the cell has been fully recycled. you have a new item to render/load
            if (this.BindingContext is MyItem item)
            {
                // Update the UI, let the user know the data is being loaded
                item.IsFetchingData = true;
                item.Status = "Not Loaded";

                // KEY TAKEAWAY
                await GetLongRunningData(item);

                // Update the UI again, letting the user know it is done
                item.IsFetchingData = false;
                item.Status = "Loaded";
            }
        }

        private async Task GetLongRunningData(MyItem item)
        {
            // DO YOUR LONG RUNNING WORK OFF THE UI THREAD HERE
            await Task.Delay(1500);

            // Remember to dispatch back to UI thread if you need to update data-bound values
            Device.BeginInvokeOnMainThread(() =>
            {
                item.Description = $"Completed at {DateTime.Now:h:mm:ss tt}.";
            });
        }
    }
}

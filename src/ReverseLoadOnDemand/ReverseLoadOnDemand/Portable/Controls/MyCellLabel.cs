using ReverseLoadOnDemand.Portable.Models;
using Xamarin.Forms;

namespace ReverseLoadOnDemand.Portable.Controls
{
    public class MyCellLabel : Label
    {
        protected override void OnBindingContextChanged()
        {
            // If the BindingContext is null, the container was recently scrolled off the screen and is being recycled
            if (BindingContext == null) return;

            // If the BindingContext is an instance of your data item, then the container was recently scrolled into the viewport.
            if (BindingContext is Item myDataItem)
            {
                // For example, the ID of the data item is useful is it indicated a consecutive order
                var newlyRenderedRowId = myDataItem.Id;

                // You could use the ID to decide if you need to fetch older items from the data service
                App.DataService.ScrollableView.OnItemVisualized(newlyRenderedRowId);
            }
        }
    }
}
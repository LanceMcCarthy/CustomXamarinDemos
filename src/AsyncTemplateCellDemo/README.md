## Example: Async Data Loading in Cell Template

If you need to load data inside any list's item, you do not want to use IValueConverter because it blocks the UI thread. Additionally, you do not want to perform any async code inside the IValueConverter as a hack (lots of unforeseen side effects).

The solution is to subclass the ListviewTemplateCell and override OnBindingContextChanged. 
this demo shows one appraoch to achieve this, there are several alternative variants but hte concept is the same:

1. Listen for the BindingContext of the DataTemplate to change
2. If the new BindingContext is not null, make your asynchronous call.
3. Update the data item before an after the call so the user can see the status in real time

Here's the result at runtime:

![image](https://user-images.githubusercontent.com/3520532/96622330-ceb26b00-12d7-11eb-82c7-014470318c05.png)

### Code

Here's the demo's example cell

```csharp
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
```
### XAML
```xml
<telerikDataControls:RadListView.ItemTemplate>
    <DataTemplate x:DataType="local:MyItem">
        <local:MyListViewTemplateCell>
            <local:MyListViewTemplateCell.View>
                <Grid Margin="20">
                    <StackLayout>
                        <Label Text="{Binding CreatedBy, StringFormat='Created by: {0}'}" />
                        <Label Text="{Binding Recipient, StringFormat='Recipient: {0}'}" />
                        <Label Text="{Binding Status, StringFormat='Status: {0}'}" TextColor="{Binding Status, Converter={StaticResource StatusToColorConv}}" />
                        <Label Text="{Binding Description}" />
                    </StackLayout>

                    <telerikPrimitives:RadBusyIndicator x:Name="LoadingIndicator"
                                                        IsBusy="{Binding IsFetchingData}"
                                                        IsVisible="{Binding IsFetchingData}"
                                                        AnimationType="Animation9"/>
                </Grid>
            </local:MyListViewTemplateCell.View>
        </local:MyListViewTemplateCell>
    </DataTemplate>
</telerikDataControls:RadListView.ItemTemplate>
```
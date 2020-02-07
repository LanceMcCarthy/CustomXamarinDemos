# Custom Segmented Control

This example shows how to build a custom control that dynamically generates segments to create a segmented control. It combines the features of the UI for Xamarin [RadButton](https://docs.telerik.com/devtools/xamarin/controls/button/button-overview) and [RadBorder](https://docs.telerik.com/devtools/xamarin/controls/border/border-overview) into a single control.

## Example

```xml
<controls:ButtonSegments ItemsSource="{Binding MyItems}"
                         SelectedIndex="{Binding MySelectedIndex, Mode=TwoWay}"
                         SelectedSegmentTextColor="#036ecb"
                         SelectedSegmentBackgroundColor="#e0edf8"
                         BackgroundColor="White"
                         BorderColor="#e1e1e1"
                         CornerRadius="4">
```

* The control is located in *SegmentedCustomControl -> Portable -> Controls* folder
* Find the above example in *SegmentedCustomControl -> Portable -> Views* folder on MainPage.xaml

On Android:

![SegmentCustomControl](https://content.screencast.com/users/lance.mccarthy/folders/Snagit/media/1568d226-7fd3-4be2-80b3-17cbc87065f7/02.06.2020-19.32.GIF)

## Customization
Because the segments are actually `RadButton` instances, they can be further customized. Go to **Line 139** on **ButtonSegments.xaml.cs** to find the button's creation. You can add your customization to that `button` variable.

For example, if you wanted to change the FontSize of the text:

```csharp
// This is Line 139 in ButtonSegments.xaml.cs
var button = new RadButton();
button.Text = item;

// Your extra customization
button.FontSize = 12;
button.FontAttributes = FontAttributes.Italic;
```
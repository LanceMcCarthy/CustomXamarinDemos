# Custom Xamarin.Forms Demos

This is a set of demos for custom use and edge-case scenarios.

| Demo Overview | Source || Demo Overview | Source |
|------|------|-|------|------|
| [PdfViewer Document Signature](https://github.com/LanceMcCarthy/CustomXamarinDemos#PdfViewer%20With%20RadSignaturePad) | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/PdfViewerWithSignaturePad) || [Asynchronous DataTemplate](https://github.com/LanceMcCarthy/CustomXamarinDemos#AsyncTemplateCellDemo) | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/AsyncTemplateCellDemo) |
| [RichTextEditor Document Signature](https://github.com/LanceMcCarthy/CustomXamarinDemos#RichTextEditor%20Document%20Signtature) | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/SignaturePanel) || [Reverse LoadOnDemand](https://github.com/LanceMcCarthy/CustomXamarinDemos#Reverse%20LoadOnDemand) | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/ReverseLoadOnDemand) |
| [SignalR and ConversationalUI](https://github.com/LanceMcCarthy/CustomXamarinDemos#SignalR%20and%20ConversationalUI) | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/SignalRChatDemo) || [ReportViewer Control](https://github.com/LanceMcCarthy/CustomXamarinDemos#ReportViewer)  | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/XFReportViewerDemo) |
| [IconGenerator Helper](https://github.com/LanceMcCarthy/CustomXamarinDemos#IconGenerator) | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/IconAssetGenerator) || [Render Image Demo](https://github.com/LanceMcCarthy/CustomXamarinDemos#renderimage) | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/RenderImage) |
| [Custom Realtime Filtering](https://github.com/LanceMcCarthy/CustomXamarinDemos#RealTimeFiltering) | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/RealTimeFilteringDemos) || [Custom Segmented Control](https://github.com/LanceMcCarthy/CustomXamarinDemos#SegmentCustomControl) | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/SegmentedCustomControl) |
| [Cascading AutoCompleteViews](https://github.com/LanceMcCarthy/CustomXamarinDemos#CascadingAutoCompleteViews) | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/CascadingAutoCompleteViews) || [Drawer Dismiss Effect](https://github.com/LanceMcCarthy/CustomXamarinDemos#DrawerDismissEffect) | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/DrawerDismissEffect) |
| [Custom Series Demo](https://github.com/LanceMcCarthy/CustomXamarinDemos#customserieslabels) | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/CustomSeriesLabels) || [Android Call Detector](https://github.com/LanceMcCarthy/CustomXamarinDemos#AndroidCallDetector)  | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/CallDetector) |
| [Telerik Theme Editor](https://github.com/LanceMcCarthy/CustomXamarinDemos#TelerikThemeEditor) | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/TelerikThemeEditor) || [Workouts](https://github.com/LanceMcCarthy/CustomXamarinDemos#Workouts) | [code](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/Workouts) |

## PdfViewer With RadSignaturePad

This demo uses the PdfDocumentProcessing libraries to locate a SignatureWidget in the RadFixedDocument and replaces it with the written signature in a RadSignaturePad.

![signaturepad-pdf-insert-written-signature](https://user-images.githubusercontent.com/3520532/153298646-0435899e-46c8-4226-8434-5b259bcfcb21.png)

## RichTextEditor Document Signature

This demo shows you how to insert a hand-drawn signature into a RichTextEditor document. From there, you can export the rich document to DOCX Word document format.

![image](https://user-images.githubusercontent.com/3520532/144868496-16625e39-7175-4dba-8efe-a712c8e3acf2.png)

## SignalR and ConversationalUI [![Build and deploy .NET Core app to Windows WebApp UIforXamarinChatServer](https://github.com/LanceMcCarthy/CustomXamarinDemos/actions/workflows/release-signalrdemo.yml/badge.svg)](https://github.com/LanceMcCarthy/CustomXamarinDemos/actions/workflows/release-signalrdemo.yml)

A demo that showcases the use of **SignalR** with the UI for Xamarin `RadChat` control. The source code also includes the SignalR Hub project (ASP.NET Core 2.2 application).

![SignalR Demo](https://user-images.githubusercontent.com/3520532/60218868-a1b68b00-983e-11e9-8bec-9d6c934e90b5.png)

## Reverse LoadOnDemand

This demo project shows you how to implement your own data virtualization feature by dynamically fetching the **previous** or **next** set of data items as the user is scrolling. This effectively gives you both normal and reverse LoadOnDemand.

![reverse-lod](https://user-images.githubusercontent.com/3520532/164092713-f55cb393-8bf9-4216-88fc-a630e19baf46.png)

## AsyncTemplateCellDemo

This project shows how to asynchronously load additional data when a ListView cell loads, without blocking the UI thread.

![image](https://user-images.githubusercontent.com/3520532/96622330-ceb26b00-12d7-11eb-82c7-014470318c05.png)

## IconGenerator

A helper app that lets you quickly generate different icon sizes from a single source image. It is very extensible because you only need to add any custom icon sizes by adding an `IconDefinition` to the **IconDefinitions** collection.

```c#
// For example, if you needed a 30x30 pixel icon for the taskbar
IconDefinitions.Add(new IconDefinition
{
    PlatformName = "Desktop",
    Category = "Taskbar",
    Height = 30,
    Width = 30
});
```

Here's the results when a list of iPhone and iPad assets are loaded as IconDefinitions.

![Generator Results](https://user-images.githubusercontent.com/3520532/51133196-51082400-1802-11e9-9298-de699b23dd49.png)

## RealTimeFiltering

A demo that shows how to use a `RadEntry` and a `RadListView` to perform real-time filtering using `DelegateFilterDescriptor`.

![image](https://user-images.githubusercontent.com/3520532/48288455-326c6200-e43a-11e8-83aa-f41766b36a7d.png)

## CascadingAutoCompleteViews

A demo that shows how to connect multiple AutoCompleteView controls so that one selection sets the items of the next AutoCompleteView and automatically set the focus on the next one.

The example uses Country > State > City:

![image](https://user-images.githubusercontent.com/3520532/48288764-2e8d0f80-e43b-11e8-82b8-84ef0ce8acb7.png)

## CustomSeriesLabels

A demo that shows you how to access and customize the series labels via a native series reference in a Xamarin Platform Effect.  

#### Android Example
![Android Chart](https://user-images.githubusercontent.com/3520532/43539078-d146e190-9591-11e8-9363-8a7f7bd2da99.png)

UWP and iOS use the same white text on red background custom style.

## RenderImage

This solution contains a Dependency Service that allows you to cature any part of the UI and render it as a PNG image. See [the specialized README](https://github.com/LanceMcCarthy/CustomXamarinDemos/tree/main/src/RenderImage) for more info.

![UI Image Renderer](https://user-images.githubusercontent.com/3520532/44611891-1c9fb700-a7d2-11e8-95e1-ea0cc8b6eed6.png)

## SegmentCustomControl

This example shows how to build a custom control that dynamically generates RadButtons to create a SegmentedControl. It combines the features of the UI for Xamarin `RadButton` and `RadBorder` into a single control.

![SegmentCustomControl](https://content.screencast.com/users/lance.mccarthy/folders/Snagit/media/1568d226-7fd3-4be2-80b3-17cbc87065f7/02.06.2020-19.32.GIF)

## TelerikThemeEditor

An application that lets you set colors of Telerik brushes and create custom theme resources

## Workouts

An application with RadDataGrid and AutoCompleteView that filters the DataGrid in real time.

## AndroidCallDetector

A Xamarin.Forms Android application that uses Android P (SDK 28) telephony features via Platform Effect and DependencyService to detect incoming phone calls.

## DrawerDismissEffect

This project shows how to use a Xamarin Platform Effect to disable the RadSideDrawer's ability to dismiss the drawer when tapping outside of the drawer area.

## ReportViewer

This demo solution shows how to use a Xamarin.Forms WebView to load the Telerik Reporting HTML5 ReportViewer using a WebView and local HTML.

## RichTextEditor Insert Image Demo

Demonstrates how to insert an image into the Rich Text Editor from one of 3 sources:

1. Online image, using a URL.
2. Local file source, using a file stream
3. Hard-coded Base64 string

![image](https://user-images.githubusercontent.com/3520532/97328987-f1e79800-184c-11eb-9ef1-18c544a7e2cb.png)




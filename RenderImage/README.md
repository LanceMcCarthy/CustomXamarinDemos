# Render UI Helper
This project uses a Dependency Service to leverage the native platform's screen capture features, then encodes the image data as png and returns a byte[] to the Xamarin.Forms layer.

Status:
- UWP
- iOS
- Android

## PDF Generation

An additional experimental feature will generate a PDF document with <a href="https://github.com/LanceMcCarthy/TseExamples#uploadingtowebapi" target="_blank">a custom ASP.NET Web API backend</a> and the <a href="http://docs.telerik.com/devtools/document-processing/introduction" target="_blank">Telerik Document Processing Libraries</a>.

the image is uploaded to the Web API and added to a PDF document. That PDF is returned as a `byte[]` and displayed in **Xamarin.Forms** using the <a href="https://docs.telerik.com/devtools/xamarin/controls/pdfviewer/pdfviewer-overview" target="_blank">Telerik UI for Xamarin RadPdfViewer</a>.

## Methods

The Render method returns a `byte[]` of an png encoded image and has one overload to set a cropping region.

1. `RenderAsync()`: Returns a capture of entire screen
2. `RenderAsync(int X, int Y, int Width, int Height)`: Returns a capture of a cropped region using absolute position values
3. `RenderRelativeAsync(int X, int Y, int Width, int Height)`: Renders a capture of a cropped region using proportional position values


Options 2 and 3 give you the ability to crop any section of the screen so that you can get capture of specific content (e.g. Charts, Graphs, Scoreboards, etc).

## Runtime
Here's what the operation looks like on UWP, but it works on all three platforms.

#### Initial XamarinForms UI
![image](https://user-images.githubusercontent.com/3520532/55267477-ceb3cb80-5258-11e9-8350-2b1a86f9afbb.png)

#### Rendered Result
![image](https://user-images.githubusercontent.com/3520532/55267480-d3787f80-5258-11e9-8431-7f341009f40f.png)

#### Final PDF Document
![image](https://user-images.githubusercontent.com/3520532/55267485-d8d5ca00-5258-11e9-9143-f9be67d8b167.png)

## Setup

The Dependency Service is registered in the native projects. See the following files:

**UWP - App.xaml.cs**
```C#
protected override void OnLaunched(LaunchActivatedEventArgs e)
{
    ...
    if (rootFrame == null)
    {
        ...
        DependencyService.Register<RenderService>();
        ...
    }
}
```
**iOS - AppDelegate.cs**
```C#
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    ...

    DependencyService.Register<RenderService>();

    ...
}
```
**Android - MainActivity.cs** 
```C#

protected override void OnCreate(Bundle bundle)
{
    ...
    
    DependencyService.Register<RenderService>();

   ...
}
```






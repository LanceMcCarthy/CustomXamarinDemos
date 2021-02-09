# Render UI Helper
This project uses a Dependency Service to leverage the native platform's screen capture features, then encodes the image data as png and returns a byte[] to the Xamarin.Forms layer.

Status:
- UWP
- iOS
- Android

## PDF Generation

There are two ways to generate a PDF document containing a rendered image:

* Local - Creates the PDF in memory, inserting the rendered image and creating the document on the device.
* Remote - Uploads the image and text to a [Custom Web API](https://github.com/LanceMcCarthy/TseExamples#uploadingtowebapi) to create the PDF document, which returns a `byte[]` of the PDF file.

The resulting `byte[]` from either option is passed to the [Telerik UI for Xamarin RadPdfViewer](https://docs.telerik.com/devtools/xamarin/controls/pdfviewer/pdfviewer-overview) for display.

## Methods

The Render method returns a `byte[]` of a **png** or **jpeg** encoded image and has overloads to set a cropping region.

1. `RenderAsync(RenderEncodingOptions encodingFormat)`: Returns a capture of entire screen.
2. `RenderAsync(int X, int Y, int Width, int Height, RenderEncodingOptions encodingFormat)`: Returns a capture of a cropped region using absolute position values
3. `RenderRelativeAsync(int X, int Y, int Width, int Height, RenderEncodingOptions encodingFormat)`: Renders a capture of a cropped region using proportional position values


Options 2 and 3 give you the ability to crop any section of the screen so that you can get capture of specific content (e.g. Charts, Graphs, Scoreboards, etc). All methods have a default encoding of `RenderEncodingOptions.Png`, but you can choose `RenderEncodingOptions.Jpeg` when needed (i.e. inserting into a PDF).

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






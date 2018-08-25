# Render UI Helper
This project uses a Dependency Service to leverage the native platform's screen capture features, then encodes the image data as png and returns a byte[] to the Xamarin.Forms layer.

Status:
- UWP (complete)
- iOS (complete)
- Android (in progress - full capture works, but crop doesn't)


## Methods

The Render method returns a `byte[]` of an png encoded image and has one overload to set a cropping region.

1. `Render()`: Returns a capture of entire screen
2. `Render(int X, int Y, int Width, int Height)`: Returns a capture of a cropped region


Option 2 gives you the ability to crop any section of the screen so that you can get capture of specific content (e.g. Charts, Graphs, Scoreboards, etc).

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


## Runtime
Here's what the operation looks like on 

### UWP

![UI Image Renderer](https://user-images.githubusercontent.com/3520532/44611891-1c9fb700-a7d2-11e8-95e1-ea0cc8b6eed6.png)

### Android

![Android](https://user-images.githubusercontent.com/3520532/44612901-9a67c080-a7da-11e8-99e2-f2ffa284df97.png)

### iOS

![iOS](https://user-images.githubusercontent.com/3520532/44612975-60e38500-a7db-11e8-99a0-d800362affba.png)



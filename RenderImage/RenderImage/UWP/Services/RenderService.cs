using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using RenderImage.Portable.Services;

namespace RenderImage.UWP.Services
{
    public class RenderService : IRenderService
    {
        public async Task<byte[]> RenderAsync()
        {
            var rtb = new RenderTargetBitmap();
            await rtb.RenderAsync(Window.Current.Content);

            var pixelBuffer = await rtb.GetPixelsAsync();
            var pixels = pixelBuffer.ToArray();
            var displayInformation = DisplayInformation.GetForCurrentView();

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("temp" + ".png", CreationCollisionOption.ReplaceExisting);

            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);

                encoder.SetPixelData(BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Premultiplied,
                    (uint)rtb.PixelWidth,
                    (uint)rtb.PixelHeight,
                    displayInformation.RawDpiX,
                    displayInformation.RawDpiY,
                    pixels);

                await encoder.FlushAsync();
            }

            var buffer = await FileIO.ReadBufferAsync(file);

            return buffer.ToArray();
        }

        public async Task<byte[]> RenderAsync(int x, int y, int width, int height)
        {
            var rtb = new RenderTargetBitmap();
            await rtb.RenderAsync(Window.Current.Content);

            var pixelBuffer = await rtb.GetPixelsAsync();
            var pixels = pixelBuffer.ToArray();
            var displayInformation = DisplayInformation.GetForCurrentView();

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("temp" + ".png", CreationCollisionOption.ReplaceExisting);

            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);

                encoder.SetPixelData(BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Premultiplied,
                    (uint)rtb.PixelWidth,
                    (uint)rtb.PixelHeight,
                    displayInformation.RawDpiX,
                    displayInformation.RawDpiY,
                    pixels);

                // This does the cropping
                encoder.BitmapTransform.Bounds = new BitmapBounds
                {
                    X = (uint)x,
                    Y = (uint)y,
                    Height = (uint)height,
                    Width = (uint)width
                };

                await encoder.FlushAsync();
            }

            var buffer = await FileIO.ReadBufferAsync(file);

            return buffer.ToArray();
        }

        public async Task<byte[]> RenderRelativeAsync(int xProportion, int yProportion, int widthProportion, int heightProportion)
        {
            var rtb = new RenderTargetBitmap();
            await rtb.RenderAsync(Window.Current.Content);

            var pixelBuffer = await rtb.GetPixelsAsync();
            var pixels = pixelBuffer.ToArray();
            var displayInformation = DisplayInformation.GetForCurrentView();

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("temp" + ".png", CreationCollisionOption.ReplaceExisting);

            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);

                encoder.SetPixelData(BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Premultiplied,
                    (uint)rtb.PixelWidth,
                    (uint)rtb.PixelHeight,
                    displayInformation.RawDpiX,
                    displayInformation.RawDpiY,
                    pixels);
                
                var xTrue = rtb.PixelWidth * xProportion / 100;
                var yTrue = rtb.PixelHeight * yProportion / 100;
                var widthTrue = rtb.PixelWidth * widthProportion / 100;
                var heightTrue = rtb.PixelHeight * heightProportion / 100;

                // This does the cropping
                encoder.BitmapTransform.Bounds = new BitmapBounds
                {
                    X = (uint)xTrue,
                    Y = (uint)yTrue,
                    Height = (uint)widthTrue,
                    Width = (uint)heightTrue
                };

                await encoder.FlushAsync();
            }

            var buffer = await FileIO.ReadBufferAsync(file);

            return buffer.ToArray();
        }
    }
}

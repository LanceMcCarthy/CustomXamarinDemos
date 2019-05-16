using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using RenderImage.Portable.Models;
using RenderImage.Portable.Services;
using UIKit;

namespace RenderImage.iOS.Services
{
    public class RenderService : IRenderService
    {
        public Task<byte[]> RenderAsync(RenderEncodingOptions encodingFormat = RenderEncodingOptions.Png)
        {
            return Task.Run(() =>
            {
                var capture = UIScreen.MainScreen.Capture();

                if (encodingFormat == RenderEncodingOptions.Jpeg)
                {
                    using (var nsData = capture.AsJPEG())
                    {
                        var bytes = new byte[nsData.Length];

                        Marshal.Copy(nsData.Bytes, bytes, 0, Convert.ToInt32(nsData.Length));

                        return bytes;
                    }
                }
                else
                {
                    using (var nsData = capture.AsPNG())
                    {
                        var bytes = new byte[nsData.Length];

                        Marshal.Copy(nsData.Bytes, bytes, 0, Convert.ToInt32(nsData.Length));

                        return bytes;
                    }
                }
            });
        }

        public Task<byte[]> RenderAsync(int x, int y, int width, int height, RenderEncodingOptions encodingFormat = RenderEncodingOptions.Png)
        {
            return Task.Run(() =>
            {
                var capture = UIScreen.MainScreen.Capture();

                // Option 1
                //UIGraphics.BeginImageContext(new SizeF(width, height));
                //UIGraphics.GetCurrentContext().ClipToRect(new RectangleF(0, 0, width, height));
                //capture.Draw(new RectangleF(-x, -y, width, height));
                //var modifiedImage = UIGraphics.GetImageFromCurrentImageContext();
                //UIGraphics.EndImageContext();

                // Option 2
                // NOTE: Might need to use -x and -y
                using (var cgImage = capture.CGImage.WithImageInRect(new RectangleF(x, y, width, height)))
                {
                    var croppedImage = UIImage.FromImage(cgImage);

                    if (encodingFormat == RenderEncodingOptions.Jpeg)
                    {
                        using (var nsData = croppedImage.AsJPEG())
                        {
                            var bytes = new byte[nsData.Length];

                            Marshal.Copy(nsData.Bytes, bytes, 0, Convert.ToInt32(nsData.Length));

                            return bytes;
                        }
                    }
                    else
                    {
                        using (var nsData = croppedImage.AsPNG())
                        {
                            var bytes = new byte[nsData.Length];

                            Marshal.Copy(nsData.Bytes, bytes, 0, Convert.ToInt32(nsData.Length));

                            return bytes;
                        }
                    }
                }
            });
        }

        public Task<byte[]> RenderRelativeAsync(int xProportion, int yProportion, int widthProportion, int heightProportion, RenderEncodingOptions encodingFormat = RenderEncodingOptions.Png)
        {
            return Task.Run(() =>
            {
                var capture = UIScreen.MainScreen.Capture();

                var xTrue = capture.CGImage.Width * xProportion / 100;
                var yTrue = capture.CGImage.Height * yProportion / 100;
                var widthTrue = capture.CGImage.Width * widthProportion / 100;
                var heightTrue = capture.CGImage.Height * heightProportion / 100;

                using (var cgImage = capture.CGImage.WithImageInRect(new RectangleF(xTrue, yTrue, widthTrue, heightTrue)))
                {
                    var croppedImage = UIImage.FromImage(cgImage);

                    if (encodingFormat == RenderEncodingOptions.Jpeg)
                    {
                        using (var nsData = croppedImage.AsJPEG())
                        {
                            var bytes = new byte[nsData.Length];

                            Marshal.Copy(nsData.Bytes, bytes, 0, Convert.ToInt32(nsData.Length));

                            return bytes;
                        }
                    }
                    else
                    {
                        using (var nsData = croppedImage.AsPNG())
                        {
                            var bytes = new byte[nsData.Length];

                            Marshal.Copy(nsData.Bytes, bytes, 0, Convert.ToInt32(nsData.Length));

                            return bytes;
                        }
                    }
                }
            });
        }
    }
}
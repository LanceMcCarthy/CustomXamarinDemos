using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using RenderImage.Portable.Services;
using UIKit;

// *************** Work in progress *************** //

namespace RenderImage.iOS.Services
{
    public class RenderService : IRenderService
    {
        public async Task<byte[]> RenderAsync()
        {
            var capture = UIScreen.MainScreen.Capture();

            using (var nsData = capture.AsPNG())
            {
                var bytes = new byte[nsData.Length];

                Marshal.Copy(nsData.Bytes, bytes, 0, Convert.ToInt32(nsData.Length));

                return bytes;
            }
        }

        public async Task<byte[]> RenderAsync(int x, int y, int width, int height)
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

                using (var nsData = croppedImage.AsPNG())
                {
                    var bytes = new byte[nsData.Length];

                    Marshal.Copy(nsData.Bytes, bytes, 0, Convert.ToInt32(nsData.Length));

                    return bytes;
                }
            }
        }
    }
}
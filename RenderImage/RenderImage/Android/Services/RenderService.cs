using System.IO;
using System.Threading.Tasks;
using Android.Graphics;
using Plugin.CurrentActivity;
using RenderImage.Portable.Services;

namespace RenderImage.Android.Services
{
    public class RenderService : IRenderService
    {
        public Task<byte[]> RenderAsync()
        {
            return Task.Run(() =>
            {
                var rootView = CrossCurrentActivity.Current.Activity.Window.DecorView.RootView;

                // Generate the full screen bitmap
                using (var bitmap = Bitmap.CreateBitmap(rootView.Width, rootView.Height, Bitmap.Config.Argb8888))
                {
                    var canvas = new Canvas(bitmap);
                    rootView.Draw(canvas);

                    using (var stream = new MemoryStream())
                    {
                        bitmap.Compress(Bitmap.CompressFormat.Png, 90, stream);
                        return stream.ToArray();
                    }
                }
            });
        }

        public Task<byte[]> RenderAsync(int x, int y, int width, int height)
        {
            return Task.Run(() =>
            {
                var rootView = CrossCurrentActivity.Current.Activity.Window.DecorView.RootView;

                // Generate the full screen bitmap
                using (var bitmap = Bitmap.CreateBitmap(rootView.Width, rootView.Height, Bitmap.Config.Argb8888))
                {
                    var canvas = new Canvas(bitmap);
                    rootView.Draw(canvas);

                    // Generate the cropped bitmap using the crop rect.
                    using(var croppedBitmap = Bitmap.CreateBitmap(bitmap, x, y, width, height))
                    {
                        var canvas2 = new Canvas(croppedBitmap);
                        rootView.Draw(canvas2);

                        using (var stream = new MemoryStream())
                        {
                            croppedBitmap.Compress(Bitmap.CompressFormat.Png, 90, stream);
                            return stream.ToArray();
                        }
                    }
                }
            }); 
        }
    }
}
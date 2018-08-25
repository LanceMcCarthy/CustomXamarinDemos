using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
using Plugin.CurrentActivity;
using RenderImage.Portable.Services;
using Xamarin.Forms;

// *************** Work in progress *************** //

namespace RenderImage.Android.Services
{
    public class RenderService : IRenderService
    {
        public Task<byte[]> RenderAsync()
        {
            return Task.Run(() =>
            {
                var rootView = CrossCurrentActivity.Current.Activity.Window.DecorView.RootView;

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

                using (var bitmap = Bitmap.CreateBitmap(rootView.Width, rootView.Height, Bitmap.Config.Argb8888))
                {
                    var canvas = new Canvas(bitmap);

                    rootView.Draw(canvas);

                    // TODO Crop isnt working
                    canvas.ClipRect(new Rect(x, y, x + width, y + height));

                    using (var stream = new MemoryStream())
                    {
                        bitmap.Compress(Bitmap.CompressFormat.Png, 90, stream);

                        return stream.ToArray();
                    }
                }
            });
        }
    }
}
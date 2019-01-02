using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallDetector.Portable.Helpers
{
    [ContentProperty("BackgroundImage")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string BackgroundImage { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(this.BackgroundImage))
            {
                return null;
            }

            var imageSource = ImageSource.FromResource($"CallDetector.Portable.Images.{this.BackgroundImage}");

            return imageSource;
        }
    }
}

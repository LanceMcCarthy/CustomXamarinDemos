using System;
using Windows.Storage;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace IconAssetGenerator.Uwp.Converters
{
    public class BitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var bitmapImage = new BitmapImage();

            if (value is StorageFile file)
            {
                bitmapImage.UriSource = new Uri(file.Path);
            }
            else
            {
                bitmapImage.UriSource = new Uri("ms-appx:///assets/WindowsLogo.png");
            }
            
            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

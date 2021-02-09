using System;
using System.IO;
using Com.Telerik.Widget.Chart.Visualization.Annotations.Cartesian;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart.Series.Categorical;
using CustomSeriesLabels.Android.ChartFeatureRenderers;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Threading.Tasks;

// [assembly: ResolutionGroupName("MyCompany")] // Already defined in another Effect, if you are only copying this class, be sure to uncomment this in your project
[assembly: ExportEffect(typeof(CustomSeriesLabels.Android.Effects.CustomAnnotationEffect), "CustomAnnotationEffect")]
namespace CustomSeriesLabels.Android.Effects
{
    public class CustomAnnotationEffect : PlatformEffect
    {
        protected override async void OnAttached()
        {
            if (Control is RadCartesianChartView nativeChart && nativeChart.Series.Get(0) is BarSeries barSeries)
            {
                Bitmap myImage = await GetMyImageAsync("https://d585tldpucybw.cloudfront.net/sfimages/default-source/blogs/author-images/progress-blog-default-logo-transparent.png");

                var annotation = new CartesianCustomAnnotation(
                    nativeChart.VerticalAxis,
                    nativeChart.HorizontalAxis,
                    8,
                    "Feb",
                    myImage);

                annotation.ContentRenderer = new ImageAnnotationRenderer();

                nativeChart.Annotations.Add(annotation);
            }
        }

        protected override void OnDetached()
        {
        }

        private static async Task<Bitmap> GetMyImageAsync(string imgUrl)
        {
            var fileName = System.IO.Path.GetFileName(imgUrl);
            var localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var imgPath = System.IO.Path.Combine(localFolder, fileName);

            var request = new System.Net.HttpWebRequest(new Uri(imgUrl));
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                using (var fs = File.OpenWrite(imgPath))
                {
                    await stream.CopyToAsync(fs);
                    await stream.FlushAsync();
                }
            }

            var imgBitmap = await BitmapFactory.DecodeFileAsync(imgPath, new BitmapFactory.Options { InJustDecodeBounds = false });
            return imgBitmap;
        }
    }
}
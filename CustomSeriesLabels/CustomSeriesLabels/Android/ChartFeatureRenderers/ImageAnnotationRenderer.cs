using Android.Graphics;
using Com.Telerik.Android.Common.Math;
using Com.Telerik.Widget.Chart.Engine.Decorations.Annotations.Custom;

namespace CustomSeriesLabels.Android.ChartFeatureRenderers
{
    public class ImageAnnotationRenderer : Java.Lang.Object, ICustomAnnotationRenderer
    {
        public RadSize MeasureContent(Java.Lang.Object content)
        {
            if (content == null)
            {
                return RadSize.Empty;
            }

            // Cast the content as Bitmap
            var imgBitmap = (Bitmap)content;

            // Get the bitmap dimensions to measure the size of the contents.
            return new RadSize(imgBitmap.Width, imgBitmap.Height);
        }

        public void Render(
            Java.Lang.Object content,
            RadRect layoutSlot,
            Canvas canvas,
            Paint paint)
        {
            if (content == null)
            {
                return;
            }

            // Cast the content as Bitmap
            var imgBitmap = (Bitmap)content;

            // Draw the bitmap to the Canvas
            canvas.DrawBitmap(
                imgBitmap, 
                (float)layoutSlot.GetX() - (float)(layoutSlot.Width / 2.0),
                (float)layoutSlot.Bottom - (float)layoutSlot.Height / 2,
                paint);

        }
    }
}
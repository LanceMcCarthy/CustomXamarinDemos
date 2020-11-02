using Android.Graphics;
using Com.Telerik.Widget.Chart.Engine.DataPoints;
using Com.Telerik.Widget.Chart.Engine.ElementTree;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart.Series.Categorical;
using Com.Telerik.Widget.Chart.Visualization.Common;
using Telerik.XamarinForms.Common.Android;
using Color = Android.Graphics.Color; // Disambiguate .NET or Xamarin.Forms Color

namespace CustomSeriesLabels.Android.ChartFeatureRenderers
{
    class MyHorizontalLabelRenderer : CategoricalSeriesLabelRenderer
    {
        public MyHorizontalLabelRenderer(ChartSeries p0) : base(p0)
        {

        }

        public override void RenderLabel(Canvas p0, ChartNode p1)
        {
            base.RenderLabel(p0, p1);
            DataPoint dataPoint = (DataPoint)p1;
            p0.DrawCircle((float)dataPoint.CenterX, (float)dataPoint.CenterY, 10, new Paint() { Color = Color.Red });
        }

        protected override string GetLabelText(DataPoint p0)
        {
            var convertibleObject = (ConvertibleObject<object>)p0.DataItem;
            var categoricalData = (CustomSeriesLabels.Portable.Models.CategoricalData)convertibleObject.Instance;
            return categoricalData.Category.ToString();
        }
    }
}
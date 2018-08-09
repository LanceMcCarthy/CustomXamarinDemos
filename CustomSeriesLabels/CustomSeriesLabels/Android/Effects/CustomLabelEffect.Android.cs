using Android.Graphics;
using Com.Telerik.Widget.Chart.Engine.DataPoints;
using Com.Telerik.Widget.Chart.Engine.ElementTree;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart.Series.Categorical;
using Com.Telerik.Widget.Chart.Visualization.Common;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

// Disambiguate .NET or Xamarin.Forms Color
using Color = Android.Graphics.Color;

[assembly: ResolutionGroupName("MyCompany")]
[assembly: ExportEffect(typeof(CustomSeriesLabels.Android.Effects.CustomLabelEffect), "CustomLabelEffect")]
namespace CustomSeriesLabels.Android.Effects
{
    public class CustomLabelEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control is RadCartesianChartView nativeChart)
            {
                // Iterate over the native chart's series
                for (int i = 0; i < nativeChart.Series.Size(); i++)
                {
                    // Get a reference to the series you want
                    if (nativeChart.Series.Get(i) is SplineAreaSeries series)
                    {
                        // set the Label properties you want
                        series.LabelFillColor = Color.Red;
                        series.LabelTextColor = Color.White;

                        series.LabelRenderer = new MyLabelRenderer(series);
                    }
                }
            }
        }

        protected override void OnDetached()
        {
        }
    }

    public class MyLabelRenderer : CategoricalSeriesLabelRenderer
    {
        public MyLabelRenderer(ChartSeries p0) : base(p0)
        {

        }

        public override void RenderLabel(Canvas p0, ChartNode p1)
        {
            base.RenderLabel(p0, p1);
            DataPoint dataPoint = (DataPoint)p1;
            p0.DrawCircle((float)dataPoint.CenterX, (float)dataPoint.CenterY, 10, new Paint() { Color = Color.Red });
        }
    }
}
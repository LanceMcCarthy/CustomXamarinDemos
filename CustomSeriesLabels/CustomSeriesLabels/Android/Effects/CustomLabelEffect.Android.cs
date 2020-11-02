using Com.Telerik.Widget.Chart.Visualization.CartesianChart;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart.Series.Categorical;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart.Series.Scatter;
using CustomSeriesLabels.Android.ChartFeatureRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color; // Disambiguate .NET or Xamarin.Forms Color

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
                    // SplineArea series
                    if (nativeChart.Series.Get(i) is SplineAreaSeries series)
                    {
                        // set the Label properties you want
                        series.LabelFillColor = Color.Red;
                        series.LabelTextColor = Color.White;

                        series.LabelRenderer = new MyHorizontalLabelRenderer(series);
                    }

                    // Bar series
                    if (nativeChart.Series.Get(i) is BarSeries barSeries)
                    {
                        // set the Label properties you want
                        barSeries.LabelFillColor = Color.Red;
                        barSeries.LabelTextColor = Color.White;

                        barSeries.LabelRenderer = new MyVerticalLabelRenderer(barSeries);
                    }

                    if (nativeChart.Series.Get(i) is ScatterLineSeries scatterLineSeries)
                    {
                        scatterLineSeries.DataPointRenderer = new MyScatterPointRenderer(scatterLineSeries);
                    }
                }
            }
        }

        protected override void OnDetached()
        {
        }
    }
}
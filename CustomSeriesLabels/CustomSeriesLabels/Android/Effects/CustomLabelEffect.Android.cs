using Com.Telerik.Widget.Chart.Visualization.CartesianChart;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart.Series.Categorical;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
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
                    }
                }
            }
        }

        protected override void OnDetached()
        {
        }
    }
}
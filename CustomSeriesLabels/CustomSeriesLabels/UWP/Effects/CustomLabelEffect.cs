using System.Linq;
using Telerik.UI.Xaml.Controls.Chart;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ResolutionGroupName("MyCompany")]
[assembly: ExportEffect(typeof(CustomSeriesLabels.UWP.Effects.CustomLabelEffect), "CustomLabelEffect")]
namespace CustomSeriesLabels.UWP.Effects
{
    public class CustomLabelEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (this.Element.Effects.FirstOrDefault(e => e is CustomSeriesLabels.Portable.Effects.CustomLabelEffect) is CustomSeriesLabels.Portable.Effects.CustomLabelEffect effect)
            {
                if (Control is RadCartesianChart nativeChart)
                {
                    var series = nativeChart.Series.FirstOrDefault() as SplineAreaSeries;

                    var labelDefinition = new ChartSeriesLabelDefinition();

                    // Note: This DataTemplate is defined in the UWP project's App.xaml
                    labelDefinition.Template = App.Current.Resources["CustomChartLabelTemplate"] as Windows.UI.Xaml.DataTemplate;

                    series.LabelDefinitions.Add(labelDefinition);
                }
            }
        }

        protected override void OnDetached()
        {

        }
    }
}

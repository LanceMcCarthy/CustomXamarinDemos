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
            if (this.Element.Effects.FirstOrDefault(e => e is Portable.Effects.CustomLabelEffect) is Portable.Effects.CustomLabelEffect effect)
            {
                // ********* CARTESIAN CHARTS ********* //

                if (Control is RadCartesianChart nativeChart)
                {
                    foreach (var series in nativeChart.Series)
                    {
                        series.LabelDefinitions.Clear();

                        var labelDefinition = new ChartSeriesLabelDefinition();

                        if (series is SplineAreaSeries)
                        {
                            // Note: The DataTemplates must be defined in the UWP project's App.xaml file, not Xamarin.Forms App.xaml
                            labelDefinition.Template = App.Current.Resources["HorizontalLabelTemplate"] as Windows.UI.Xaml.DataTemplate;
                            
                        }

                        if (series is BarSeries)
                        {
                            // Note: The DataTemplates must be defined in the UWP project's App.xaml file, not Xamarin.Forms App.xaml
                            labelDefinition.Template = App.Current.Resources["VerticalLabelTemplate"] as Windows.UI.Xaml.DataTemplate;
                        }

                        series.LabelDefinitions.Add(labelDefinition);
                    }
                }

                // ********* PIE CHARTS ********* //

                if (Control is RadPieChart nativePieChart)
                {
                    // 1. Clear the default label definition
                    nativePieChart.Series[0].LabelDefinitions.Clear();

                    // 2. Add a new label definition that uses the DataTemplate (must be in the UWP project's app.xaml)
                    nativePieChart.Series[0].LabelDefinitions.Add(new ChartSeriesLabelDefinition
                    {
                        Template = App.Current.Resources["PieLabelTemplate"] as Windows.UI.Xaml.DataTemplate
                    });
                }
            }
        }

        protected override void OnDetached()
        {

        }
    }
}

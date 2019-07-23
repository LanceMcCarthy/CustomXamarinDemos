﻿using System.Linq;
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
            }
        }

        protected override void OnDetached()
        {

        }
    }
}

using Telerik.XamarinForms.ChartRenderer.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("MyCompany")]
[assembly: ExportEffect(typeof(CustomSeriesLabels.iOS.Effects.CustomLabelEffect), "CustomLabelEffect")]
namespace CustomSeriesLabels.iOS.Effects
{
    public class CustomLabelEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            TKExtendedChart nativeChart = (TKExtendedChart)this.Control;

            nativeChart.Delegate = new MyChartDelegate();
        }

        protected override void OnDetached()
        {

        }
    }
}
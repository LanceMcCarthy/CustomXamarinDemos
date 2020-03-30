using System;
using System.Diagnostics;
using System.Linq;
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
            try
            {
                var effect = (Portable.Effects.CustomLabelEffect)Element.Effects.FirstOrDefault(e => e is Portable.Effects.CustomLabelEffect);

                if(effect == null)
                    return;

                ((TKExtendedChart)this.Control).Delegate = new MyChartDelegate(effect.RotateLabels);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"CustomLabelEffect Error: {ex}");
            }
        }

        protected override void OnDetached()
        {

        }
    }
}
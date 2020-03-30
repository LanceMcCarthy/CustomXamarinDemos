using Xamarin.Forms;

namespace CustomSeriesLabels.Portable.Effects
{
    public class CustomLabelEffect : RoutingEffect
    {
        public CustomLabelEffect()
            : base("MyCompany.CustomLabelEffect")
        {
        }

        public bool RotateLabels { get; set; }
    }
}
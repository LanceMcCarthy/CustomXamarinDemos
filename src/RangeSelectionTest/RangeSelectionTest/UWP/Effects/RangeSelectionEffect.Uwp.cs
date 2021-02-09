using System.Linq;
using Telerik.UI.Xaml.Controls.Input;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ResolutionGroupName("LancelotSoftware")]
[assembly: ExportEffect(typeof(RangeSelectionTest.UWP.Effects.RangeSelectionEffect), "RangeSelectionEffect")]
namespace RangeSelectionTest.UWP.Effects
{
    public class RangeSelectionEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Element.Effects.FirstOrDefault (e => e is Portable.Effects.RangeSelectionEffect) is Portable.Effects.RangeSelectionEffect effect)
            {
                effect.DateRangeValueChanged += Effect_DateRangeValueChanged;

                if (Control is RadCalendar calendar)
                {
                    calendar.SelectionMode = CalendarSelectionMode.Multiple;
                }
            }   
        }
        

        private void Effect_DateRangeValueChanged(object sender, Portable.Effects.DateRangeChangedEventArgs args)
        {
            if (Control is RadCalendar calendar)
            {
                var dateRange = new CalendarDateRange
                {
                    StartDate = args.StartDate,
                    EndDate = args.EndDate
                };
                
                calendar.SelectedDateRange = dateRange;
            }
        }

        protected override void OnDetached()
        {
            if (Element.Effects.FirstOrDefault (e => e is Portable.Effects.RangeSelectionEffect) is Portable.Effects.RangeSelectionEffect effect)
            {
                effect.DateRangeValueChanged -= Effect_DateRangeValueChanged;
            }    
        }
    }
}

using System;
using System.Linq;
using Foundation;
using TelerikUI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("LancelotSoftware")]
[assembly: ExportEffect(typeof(RangeSelectionTest.iOS.Effects.RangeSelectionEffect), "RangeSelectionEffect")]
namespace RangeSelectionTest.iOS.Effects
{
    public class RangeSelectionEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Element.Effects.FirstOrDefault (e => e is Portable.Effects.RangeSelectionEffect) is Portable.Effects.RangeSelectionEffect effect)
            {
                effect.DateRangeValueChanged += Effect_DateRangeValueChanged;

                if (Control is TKCalendar calendar)
                {
                    calendar.SelectionMode = TKCalendarSelectionMode.Range;
                }
            }  
        }

        private void Effect_DateRangeValueChanged(object sender, Portable.Effects.DateRangeChangedEventArgs args)
        {
            if (Control is TKCalendar calendar)
            {
                calendar.SelectedDatesRange = new TKDateRange
                {
                    StartDate = ToNSDate(args.StartDate),
                    EndDate = ToNSDate(args.EndDate)
                };
            }
        }

        protected override void OnDetached()
        {
            if (Element.Effects.FirstOrDefault (e => e is Portable.Effects.RangeSelectionEffect) is Portable.Effects.RangeSelectionEffect effect)
            {
                effect.DateRangeValueChanged -= Effect_DateRangeValueChanged;
            }  
        }
        
        // ReSharper disable once InconsistentNaming
        private static NSDate ToNSDate(DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
                date = DateTime.SpecifyKind(date, DateTimeKind.Local);

            return (NSDate) date;
        }
    }
}
using System;
using System.Linq;
using Com.Telerik.Widget.Calendar;
using Java.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("LancelotSoftware")]
[assembly: ExportEffect(typeof(RangeSelectionTest.Android.Effects.RangeSelectionEffect), "RangeSelectionEffect")]
namespace RangeSelectionTest.Android.Effects
{
    public class RangeSelectionEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Element.Effects.FirstOrDefault (e => e is Portable.Effects.RangeSelectionEffect) is Portable.Effects.RangeSelectionEffect effect)
            {
                effect.DateRangeValueChanged += Effect_DateRangeValueChanged;

                if (Control is RadCalendarView calendar)
                {
                    calendar.SelectionMode = CalendarSelectionMode.Range;
                }
            }  
        }

        private void Effect_DateRangeValueChanged(object sender, Portable.Effects.DateRangeChangedEventArgs args)
        {
            if (Control is RadCalendarView calendarView)
            {
                calendarView.SelectedRange = new DateRange(ConvertToCalendar(args.StartDate).TimeInMillis, ConvertToCalendar(args.EndDate).TimeInMillis);
            }
        }

        protected override void OnDetached()
        {
            if (Element.Effects.FirstOrDefault (e => e is Portable.Effects.RangeSelectionEffect) is Portable.Effects.RangeSelectionEffect effect)
            {
                effect.DateRangeValueChanged -= Effect_DateRangeValueChanged;
            }    
        }

        public static Calendar ConvertToCalendar(DateTime date) 
        {
            Calendar calendar = Calendar.Instance;
            calendar.Set(date.Year, date.Month - 1, date.Day, date.Hour, date.Minute, date.Second);
            return calendar;
        } 
    }
}
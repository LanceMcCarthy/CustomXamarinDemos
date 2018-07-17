using System;

namespace RangeSelectionTest.Portable.Effects
{
    public class DateRangeChangedEventArgs : System.EventArgs
    {
        public DateRangeChangedEventArgs(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}

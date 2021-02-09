using System;
using Xamarin.Forms;

namespace RangeSelectionTest.Portable.Effects
{
    public class RangeSelectionEffect : RoutingEffect
    {
        private DateTime endDate;
        private DateTime startDate;

        public RangeSelectionEffect() 
            : base("LancelotSoftware.RangeSelectionEffect")
        {
        }

        public DateTime StartDate
        {
            get => startDate;
            set
            {
                if (startDate == value)
                    return;

                startDate = value;
                ExecuteValueChanged();
            }
        }

        public DateTime EndDate
        {
            get => endDate;
            set
            {
                if (endDate == value)
                    return;

                endDate = value;
                ExecuteValueChanged();
            }
        }

        public delegate void DateRangeChanged(object sender, DateRangeChangedEventArgs e);

        public event DateRangeChanged DateRangeValueChanged;

        private void ExecuteValueChanged()
        {
            DateRangeValueChanged?.Invoke(this, new DateRangeChangedEventArgs(this.StartDate, this.EndDate));
        }
    }
}

using CommonHelpers.Common;

namespace CustomSeriesLabels.Portable.Models
{
    public class CategoricalData : BindableBase
    {
        public object Category { get; set; }
        public double Value { get; set; }
    }
}
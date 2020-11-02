using System.Collections.ObjectModel;
using CustomSeriesLabels.Portable.Models;

namespace CustomSeriesLabels.Portable.ViewModels
{
    public class AnnotationsViewModel
    {
        public AnnotationsViewModel()
        {
            BarSeriesData = new ObservableCollection<CategoricalData>
            {
                new CategoricalData { Category = "Jan", Value = 12},
                new CategoricalData { Category = "Feb", Value = 5 },
                new CategoricalData { Category = "Mar", Value = 10 },
                new CategoricalData { Category = "Apr", Value = 7 },
            };
        }

        public ObservableCollection<CategoricalData> BarSeriesData { get; set; }
    }
}

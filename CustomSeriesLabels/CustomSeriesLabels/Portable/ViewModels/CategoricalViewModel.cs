using System.Collections.ObjectModel;
using CommonHelpers.Common;
using CustomSeriesLabels.Portable.Models;

namespace CustomSeriesLabels.Portable.ViewModels
{
    public class CategoricalViewModel : ViewModelBase
    {
        public CategoricalViewModel()
        {
            SplineAreaSeriesData = new ObservableCollection<CategoricalData>
            {
                new CategoricalData { Category = "One", Value = 6},
                new CategoricalData { Category = "Two", Value = 7 },
                new CategoricalData { Category = "Three", Value = 5 },
                new CategoricalData { Category = "Four", Value = 7 },
                new CategoricalData { Category = "Five", Value = 6 },
            };

            BarSeriesData = new ObservableCollection<CategoricalData>();

            foreach (var item in SplineAreaSeriesData)
            {
                BarSeriesData.Add(new CategoricalData { Category = item.Category, Value = item.Value - 2});
            }
        }

        public ObservableCollection<CategoricalData> SplineAreaSeriesData { get; set; }

        public ObservableCollection<CategoricalData> BarSeriesData { get; set; }
    }
}
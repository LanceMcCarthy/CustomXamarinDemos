using System;
using System.Collections.ObjectModel;
using CommonHelpers.Common;
using CustomSeriesLabels.Portable.Models;

namespace CustomSeriesLabels.Portable.ViewModels
{
    public class BarViewModel : ViewModelBase
    {
        public BarViewModel()
        {
            SplineAreaSeriesData = new ObservableCollection<CategoricalData>
            {
                new CategoricalData { Category = "Long Description for DataPoint One", Value = 6},
                new CategoricalData { Category = "Long Description for DataPoint Two", Value = 7 },
                new CategoricalData { Category = "Long Description for DataPoint Three", Value = 5 },
                new CategoricalData { Category = "Long Description for DataPoint Four", Value = 7 },
                new CategoricalData { Category = "Long Description for DataPoint Five", Value = 6 },
            };

            BarSeriesData = new ObservableCollection<CategoricalData>
            {
                new CategoricalData { Category = "Long Description for DataPoint One", Value = 6},
                new CategoricalData { Category = "Long Description for DataPoint Two", Value = 7 },
                new CategoricalData { Category = "Long Description for DataPoint Three", Value = 5 },
                new CategoricalData { Category = "Long Description for DataPoint Four", Value = 7 },
                new CategoricalData { Category = "Long Description for DataPoint Five", Value = 6 },
            };

            ScatterSeriesData = new ObservableCollection<ScatterData>();

            for (double x = 0; x < 30; x = x + 0.2)
            {
                ScatterSeriesData.Add(new ScatterData { PointX = x, PointY = Math.Sin(x) });
            }
        }

        public ObservableCollection<CategoricalData> SplineAreaSeriesData { get; set; }

        public ObservableCollection<CategoricalData> BarSeriesData { get; set; }

        public ObservableCollection<ScatterData> ScatterSeriesData { get; set; }
    }
}

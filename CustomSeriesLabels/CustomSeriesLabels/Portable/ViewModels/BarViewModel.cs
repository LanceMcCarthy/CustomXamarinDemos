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
            GenerateCategoricalData();
            GenerateScatterData();
        }

        public ObservableCollection<CategoricalData> SplineAreaSeriesData { get; set; }

        public ObservableCollection<CategoricalData> BarSeriesData { get; set; }


        public ObservableCollection<ScatterData> ScatterSeriesData { get; set; }

        private void GenerateCategoricalData()
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
                BarSeriesData.Add(new CategoricalData { Category = item.Category, Value = item.Value - 2 });
            }
        }

        private void GenerateScatterData()
        {
            ScatterSeriesData = new ObservableCollection<ScatterData>();

            for (double x = 0; x < 30; x = x + 0.2)
            {
                ScatterSeriesData.Add(new ScatterData { PointX = x, PointY = Math.Sin(x) });
            }
        }
    }
}

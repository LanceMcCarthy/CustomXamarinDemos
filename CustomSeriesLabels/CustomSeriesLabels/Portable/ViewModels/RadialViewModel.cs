using System.Collections.ObjectModel;
using CommonHelpers.Common;
using CustomSeriesLabels.Portable.Models;

namespace CustomSeriesLabels.Portable.ViewModels
{
    public class RadialViewModel : ViewModelBase
    {
        public RadialViewModel()
        {
            GenerateRadialData();
        }

        public ObservableCollection<CategoricalData> DonutSeriesData { get; set; }

        public ObservableCollection<CategoricalData> PieSeriesData { get; set; }

        private void GenerateRadialData()
        {
            DonutSeriesData = new ObservableCollection<CategoricalData>
            {
                new CategoricalData { Category = "Mercedes Benz", Value = 25},
                new CategoricalData { Category = "BMW", Value = 40 },
                new CategoricalData { Category = "Alfa Romeo", Value = 35 },
            };


            PieSeriesData = new ObservableCollection<CategoricalData>
            {
                new CategoricalData { Category = "Apples", Value = 30},
                new CategoricalData { Category = "Oranges", Value = 40 },
                new CategoricalData { Category = "Pears", Value = 28 },
                new CategoricalData { Category = "Bananas", Value = 2 },
            };
        }

    }
}

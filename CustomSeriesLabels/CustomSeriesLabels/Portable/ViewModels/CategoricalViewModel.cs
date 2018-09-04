using System.Collections.ObjectModel;
using CustomSeriesLabels.Portable.Models;

namespace CustomSeriesLabels.Portable.ViewModels
{
    public class CategoricalViewModel
    {
        public CategoricalViewModel() { }

        public ObservableCollection<CategoricalData> CategoricalData { get; set; } = new ObservableCollection<CategoricalData>
        {
            new CategoricalData { Category = "One", Label = "Item 1", Value = 1.1},
            new CategoricalData { Category = "Two", Label = "Item 2", Value = 1.5 },
            new CategoricalData { Category = "Three", Label = "Item 3", Value = 1.2 },
            new CategoricalData { Category = "Four", Label = "Item 4", Value = 1.4 },
            new CategoricalData { Category = "Five", Label = "Item 5", Value = 1.8 },
            new CategoricalData { Category = "Six", Label = "Item 6", Value = 1.0 }
        };
    }
}
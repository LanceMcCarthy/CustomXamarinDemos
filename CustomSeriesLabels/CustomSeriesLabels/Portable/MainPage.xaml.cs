using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CustomSeriesLabels.Portable
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }

    public class CategoricalData
    {
        public object Category { get; set; }
        public double Value { get; set; }
    }

    public class CategoricalViewModel
    {
        public CategoricalViewModel()
        {
        }

        public ObservableCollection<CategoricalData> CategoricalData { get; set; } = new ObservableCollection<CategoricalData>
        {
            new CategoricalData { Category = "One", Value = 1.1 },
            new CategoricalData { Category = "Two", Value = 1.5 },
            new CategoricalData { Category = "Three", Value = 1.2 },
            new CategoricalData { Category = "Four", Value = 1.4 },
            new CategoricalData { Category = "Five", Value = 1.8 },
            new CategoricalData { Category = "Six", Value = 1.0 }
        };

    }
}

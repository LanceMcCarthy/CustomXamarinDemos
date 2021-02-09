using System.Collections.ObjectModel;
using System.Linq;
using Telerik.XamarinForms.Common;

namespace AsyncTemplateCellDemo.Portable
{
    public class MainViewModel : NotifyPropertyChangedBase
    {
        public MainViewModel()
        {
            // Starter items.
            // The other 2 properties will be updated when the TemplateCell asynchronously loads the data
            var starterData = Enumerable.Range(1, 50).Select(i => new MyItem
            {
                CreatedBy = $"Creator {i}",
                Recipient = $"Recipient {i}",
                Status = "Not Done"
            });

            Tasks = new ObservableCollection<MyItem>(starterData);
        }

        public ObservableCollection<MyItem> Tasks { get; set; }
    }
}
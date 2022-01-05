using System.Collections.Generic;
using System.Windows.Input;
using CommonHelpers.Common;
using Xamarin.Forms;

namespace SegmentedCustomControl.Portable.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private int mySelectedIndex;
        private List<string> myItems;
        private string mySelectedItem;

        public MainPageViewModel()
        {
            Title = "Main";

            MyItems = new List<string>
            {
                "Mail",
                "Calendar"
            };

            ChangeSelectedIndexCommand = new Command(ChangeSelectedIndex);
        }

        public List<string> MyItems
        {
            get => myItems;
            set => SetProperty(ref myItems, value);
        }

        public int MySelectedIndex
        {
            get => mySelectedIndex;
            set => SetProperty(ref mySelectedIndex, value);
        }

        public string MySelectedItem
        {
            get => mySelectedItem;
            set => SetProperty(ref mySelectedItem, value);
        }

        public ICommand ChangeSelectedIndexCommand { get; }

        private void ChangeSelectedIndex()
        {
            if (MySelectedIndex == MyItems.Count - 1)
            {
                MySelectedIndex = 0;
            }
            else
            {
                MySelectedIndex++;
            }
        }
    }
}

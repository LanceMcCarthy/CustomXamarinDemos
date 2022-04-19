using CommonHelpers.Common;

namespace ReverseLoadOnDemand.Portable
{
    public class Item : BindableBase
    {
        private int _id;
        private string _name;

        public Item() { }

        public Item(int id, string name)
        {
            _id = id;
            _name = name;
        }

        // 'Id' is conveniently the index of the data item in the original data source.
        // You may need some other way to find the index of the item in order to know what range to fetch for older items
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}
using CommonHelpers.Common;

namespace ReverseLoadOnDemand.Portable.Models;

public class Item : BindableBase
{
    private int id;
    private string name;

    public Item() { }

    public Item(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    // 'Id' is conveniently the index of the data item in the original data source.
    // You may need some other way to find the index of the item in order to know what range to fetch for older items
    public int Id
    {
        get => id;
        set => SetProperty(ref id, value);
    }

    public string Name
    {
        get => name;
        set => SetProperty(ref name, value);
    }
}
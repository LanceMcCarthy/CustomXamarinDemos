using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ReverseLoadOnDemand.Portable
{
    public class ItemDataService : IMyDataService
    {
        private readonly List<Item> data;

        public ItemDataService()
        {
            data = new List<Item>();

            // adding 5000 simple items to mock a remote API to database data source
            for (var i = 0; i < 5000; i++)
            {
                data.Add(new Item(i, $"Item {i}"));
            }
        }

        // fetches a range of items, aka a "page" or "batch", from the fake API
        public List<Item> GetItems(int offset, int count)
        {
            return data.GetRange(offset, count);
        }

        public IScollableView ScrollableView { get; set; }
    }
}
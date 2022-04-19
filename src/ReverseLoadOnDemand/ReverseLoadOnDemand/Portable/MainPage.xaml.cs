using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CommonHelpers.Collections;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.Common.Data;
using Telerik.XamarinForms.DataGrid;
using Xamarin.Forms;

namespace ReverseLoadOnDemand.Portable
{
    public partial class MainPage : ContentPage, IScollableView
    {
        private ObservableRangeCollection<Item> items;

        private int lastVisualizedItemId;
        private int currentOffset;
        private int bufferItemsCount = 40;
        private int fetchBatchCount = 100;

        public MainPage()
        {
            InitializeComponent();

            // Use the service's ScrollableView abstraction to pass reference to this view
            // so that we can trigger a method call that supports data virtualization
            App.DataService.ScrollableView = this;

            dataGrid.LoadOnDemandBufferItemsCount = bufferItemsCount;
            dataGrid.LoadOnDemand += DataGrid_LoadOnDemand;

            items = new ObservableRangeCollection<Item>();

            dataGrid.ItemsSource = items;
        }

        private void DataGrid_LoadOnDemand(object sender, Telerik.XamarinForms.DataGrid.LoadOnDemandEventArgs e)
        {
            DataVirtualization_GetNewerItems();

            e.IsDataLoaded = true;
        }

        private void DataVirtualization_GetNewerItems()
        {
            // PHASE 1 - Normal load on demand logic that gets the next batch of items

            // Get the next set of items form the API/database
            var result = App.DataService.GetItems(currentOffset, currentOffset + fetchBatchCount);

            // Use AddRange and RemoveRange wherever possible
            // this cuts down on the collectionChanged notifications and can noticably improve performance
            items.AddRange(result);

            var lastItemInCollection = items[items.Count - 1] as Item;
            currentOffset = lastItemInCollection.Id;

            // PHASE 2 - DATA VIRTUALIZATION

            // Determine if we have too many items in the collection
            var maxItemsCount = fetchBatchCount * 5; // 500 items in this demo

            // When we hit maximum number of items we want, remove the oldest batch of items
            if (items.Count > maxItemsCount)
            {
                // Determine how many items to remove (this will be 100 for this demo)
                var firstIndexToRemove = (items[0] as Item).Id;
                var lastIndexToRemove = fetchBatchCount + firstIndexToRemove;

                // Use AddRange and RemoveRange wherever possible
                items.RemoveRange(items.Where(i =>
                    i.Id >= firstIndexToRemove &&
                    i.Id < lastIndexToRemove));
            }
        }

        private void DataVirtualization_GetOlderItems()
        {
            // Determine the first index of the currently loaded data (this will not be 0 unless there is no more older data)
            var firstIndexInCurrentCollection = (items[0] as Item).Id;

            if (firstIndexInCurrentCollection == 0)
            {
                // we are at the beginning of the data, do not fetch any more or we'll get a negative index exception
                return;
            }

            // The last item of the fetch is going to be the item right before the oldest item we current have
            var endIndexToFetch = firstIndexInCurrentCollection;

            // The first item index to fetch is the batch size
            var startIndexToFetch = endIndexToFetch - fetchBatchCount;

            // Fetch this exact page from the API/service
            var result = App.DataService.GetItems(startIndexToFetch, endIndexToFetch);

            // We need to insert the items in the right order and at the right location. Unfortunately there isn't an InsertRange,
            // so the next best way to do this is to reverse the list and insert each one at the 0 index
            result.Reverse();

            foreach (var item in result)
            {
                items.Insert(0, item);
            }
        }

        public void OnItemVisualized(int newlyVisualizedItemId)
        {
            Debug.WriteLine($"- lastID: {lastVisualizedItemId}, newId: {newlyVisualizedItemId}", "OnItemVisualized");

            // If we are scrolling back up to older items, then we need to check if we have to replace items that were removed
            if (lastVisualizedItemId > newlyVisualizedItemId)
            {
                // find out the index of the oldest itm in the list
                var oldestIndex = (items[0] as Item).Id;

                // determine how far away we are from the last item
                var indexDifference = newlyVisualizedItemId - oldestIndex;

                // Determine if we are under the 
                var mustFetchPreviousItems = indexDifference < bufferItemsCount;

                // go fetch the previous 100 items
                if (mustFetchPreviousItems)
                {
                    // ****** KEY CONCEPT ****** //
                    DataVirtualization_GetOlderItems();
                }

            }

            // We are scrolling down to newer items
            if (lastVisualizedItemId < newlyVisualizedItemId)
            {
                // NOTE: we don't need to do anything here because the DataGrid's LoadOnDemand event does this for us.
                // buuuut, it you didn't want to use the built-in LoadOnDemand, this is effectively your own version 😎
            }


            // Important: always keep track of the last id
            lastVisualizedItemId = newlyVisualizedItemId;
        }
    }
}

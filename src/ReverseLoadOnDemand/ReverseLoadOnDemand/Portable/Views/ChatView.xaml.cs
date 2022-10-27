// IMPORTANT CONSIDERATION FOR FIRST-TIME VISITORS
// The code in this class is VERY condensed.
// For a more verbose and commented version of the same logic, see DataGridView.xaml.cs instead.

using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CommonHelpers.Collections;
using ReverseLoadOnDemand.Portable.Interfaces;
using ReverseLoadOnDemand.Portable.Models;
using Telerik.XamarinForms.ConversationalUI;
using Xamarin.Forms;

namespace ReverseLoadOnDemand.Portable.Views;

public partial class ChatView : ContentView, IChatView
{
    private const int BufferItemsCount = 40;
    private const int FetchBatchCount = 100;
    private const int MaximumCount = FetchBatchCount * 5;

    private readonly ObservableRangeCollection<MyChatMessage> items;
    private MyChatMessage lastItem;
    private bool fetchInProgress = false;

    public ChatView()
    {
        InitializeComponent();

        App.ChatService.ChatView = this;

        items = new ObservableRangeCollection<MyChatMessage>();

        Chat1.Author = App.ChatService.Me;
        Chat1.ItemsSource = items;
    }

    private async void Chat1_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        // This checks if the ItemsSource was just set to an empty collection. Now I can fetch the initial messages from the service
        if (e.PropertyName == nameof(RadChat.ItemsSource) && Chat1.ItemsSource is ObservableRangeCollection<MyChatMessage> { Count: 0 })
        {
            await InitializeItemsAsync();
        }
    }

    public async void OnChatItemVisualized(MyChatMessage currentItem)
    {
        if (lastItem != null && !fetchInProgress)
        {
            Debug.WriteLine($"- lastID: {lastItem.Id}, newId: {currentItem.Id}", "OnChatItemVisualized");

            var oldestItem = items.First();
            var delta = lastItem.Id - oldestItem.Id;

            // Scrolling up (older items)
            if (lastItem.Id > currentItem.Id)
            {
                if (delta < BufferItemsCount)
                {
                    await GetOlderItemsAsync(oldestItem.Timestamp, BufferItemsCount);
                }
            }

            // Scrolling down (newer items)
            if (lastItem.Id < currentItem.Id)
            {
                var newestItem = items.Last();
                delta = lastItem.Id - newestItem.Id;

                if (delta < BufferItemsCount)
                {
                    await GetNewerItemsAsync(lastItem.Timestamp);
                }
            }
        }
        
        // always set the last item
        lastItem = currentItem;
    }

    #region Fetching Logic

    private async Task InitializeItemsAsync()
    {
        ToggleBusyIndication(true);

        await Task.Delay(1500);
        
        items.AddRange(App.ChatService.GetLatestChatItems(FetchBatchCount));

        ToggleBusyIndication(false);
    }

    private async Task GetNewerItemsAsync(DateTime timestamp)
    {
        ToggleBusyIndication(true);

        await Task.Delay(1500);
        
        items.AddRange(App.ChatService.GetNewerChatItems(timestamp, FetchBatchCount));
        
        TryTrimOld();

        ToggleBusyIndication(false);
    }

    private async Task GetOlderItemsAsync(DateTime timestamp, int numberOfItems)
    {
        ToggleBusyIndication(true);

        await Task.Delay(1500);

        var olderItems = App.ChatService.GetOlderChatItems(timestamp, FetchBatchCount);
        
        olderItems.Reverse();

        foreach (var item in olderItems)
        {
            items.Insert(0, item);
        }

        TryTrimNew();

        ToggleBusyIndication(false);
    }

    #endregion

    #region Helpers

    private void TryTrimOld()
    {
        // if the list is getting too large, take off the oldest batch
        if (items.Count > MaximumCount)
        {
            var oldestItemId = items.First().Id;

            var tailItems = items.Where(i =>
                i.Id >= oldestItemId &&
                i.Id < oldestItemId + FetchBatchCount);
            
            items.RemoveRange(tailItems);
        }
    }

    private void TryTrimNew()
    {
        // if the list is getting too large
        if (items.Count > MaximumCount)
        {
            var latestItemId = items.Last().Id;
            
            var tailItems = items.Where(i =>
                i.Id <= latestItemId &&
                i.Id < latestItemId - FetchBatchCount);

            items.RemoveRange(tailItems);
        }
    }

    private void ToggleBusyIndication(bool state)
    {
        fetchInProgress = state;
        ProgBar.IsIndeterminate = state;
    }

    #endregion

}
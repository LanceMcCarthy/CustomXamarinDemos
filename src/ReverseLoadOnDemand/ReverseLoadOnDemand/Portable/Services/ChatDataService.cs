using System;
using ReverseLoadOnDemand.Portable.Interfaces;
using ReverseLoadOnDemand.Portable.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.XamarinForms.ConversationalUI;

namespace ReverseLoadOnDemand.Portable.Services;

public class ChatDataService : IChatService
{
    // Mocking a database or REST service data
    private readonly List<MyChatMessage> messages;

    public ChatDataService()
    {
        messages = GenerateMockData();
    }

    #region properties

    public IChatView ChatView { get; set; }

    public Author Me => new() { Name = "Me", Avatar = "Images/kendoka-02.png" };

    public Author Bot => new() { Name = "Botsie", Avatar = "Images/kendoka-09.png" };

    #endregion

    #region public methods

    public int GetLatestId()
    {
        return messages.Last().Id;
    }

    public List<MyChatMessage> GetLatestChatItems(int count)
    {
        return messages
            .GetRange(((messages.Count - 1) - count), count)
            .ToList();
    }

    public List<MyChatMessage> GetChatItems(DateTime start, DateTime end)
    {
        return messages
            .Where(m => m.Timestamp >= start && m.Timestamp <= end)
            .ToList();
    }

    public List<MyChatMessage> GetNewerChatItems(DateTime start, int total)
    {
        // TakeWhile isn't so great for this purpose
        // messages.TakeWhile((m, i) => m.Timestamp >= start && i <= total);

        return messages
            .Where(m => m.Timestamp >= start)
            .Take(total)
            .ToList();
    }

    public List<MyChatMessage> GetOlderChatItems(DateTime start, int total)
    {
        // Attempt 1. messages.TakeWhile((m, i) => m.Timestamp <= start && i <= total).ToList();
        // Attempt 2.  messages.Where(m => m.Timestamp <= start).Take(total).OrderBy(m=>m.Id).ToList();

        // Attempt 3. Find the exact position in the list and directly get that range
        var lastMatchingIndex = messages.IndexOf(messages.FindLast(m => m.Timestamp < start));

        var offset = lastMatchingIndex - total;

        if (offset < 0)
        {
            total += offset; // If offset is negative, this will ensure we also reduce the total fetched
            offset = 0; // make sure we start from the beginning
        }

        return messages.GetRange(offset, total).ToList();
    }

    public Task UploadChatItem(MyChatMessage message)
    {
        messages.Add(message);

        return Task.CompletedTask;
    }

    #endregion


    #region internal helpers

    private List<MyChatMessage> GenerateMockData(int initialCount = 2000)
    {
        var data = new List<MyChatMessage>();

        for (var i = 0; i < initialCount; i++)
        {
            var minutesToSubtract = DateTime.Now.AddMinutes(-(initialCount - i));

            data.Add(new MyChatMessage(minutesToSubtract, i)
            {
                Author = i % 2 == 0 ? Me : Bot,
                Text = $"This is message #{i}."
            });
        }
        return data;
    }
    #endregion
}
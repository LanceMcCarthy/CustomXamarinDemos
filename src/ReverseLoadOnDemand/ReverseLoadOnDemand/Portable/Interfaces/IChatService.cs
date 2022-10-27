using System;
using ReverseLoadOnDemand.Portable.Models;
using System.Collections.Generic;

namespace ReverseLoadOnDemand.Portable.Interfaces
{
    public interface IChatService
    {
        List<MyChatMessage> GetChatItems(DateTime start, DateTime end);
    }
}
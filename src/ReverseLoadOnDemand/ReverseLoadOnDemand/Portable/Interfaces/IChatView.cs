using ReverseLoadOnDemand.Portable.Models;

namespace ReverseLoadOnDemand.Portable.Interfaces;

public interface IChatView
{
    void OnChatItemVisualized(MyChatMessage currentItem);
}
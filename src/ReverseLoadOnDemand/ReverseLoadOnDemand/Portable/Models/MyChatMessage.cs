using System;
using Telerik.XamarinForms.ConversationalUI;

namespace ReverseLoadOnDemand.Portable.Models;

public class MyChatMessage : TextMessage
{
    public MyChatMessage(DateTime timestamp, int id)
    {
        this.Id = id;
        this.Timestamp = timestamp;
    }

    public int Id { get; set; }
    public DateTime Timestamp { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is MyChatMessage other)
        {
            return other.Id == this.Id;
        }
        
        return false;
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }
}
using ReverseLoadOnDemand.Portable.Models;
using Telerik.XamarinForms.ConversationalUI;
using Xamarin.Forms;

namespace ReverseLoadOnDemand.Portable.Controls;

public class MyChatLabel : Label
{
    protected override void OnBindingContextChanged()
    {
        if (BindingContext is TextMessage tMessage)
        {
            App.ChatService.ChatView.OnChatItemVisualized((MyChatMessage)tMessage.Data);
        }
    }
}
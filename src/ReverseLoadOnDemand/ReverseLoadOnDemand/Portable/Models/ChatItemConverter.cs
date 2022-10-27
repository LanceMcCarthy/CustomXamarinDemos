using System;
using System.Diagnostics;
using Telerik.XamarinForms.ConversationalUI;
using Xamarin.Forms;

namespace ReverseLoadOnDemand.Portable.Models;

public class ChatItemConverter : IChatItemConverter
{
    public ChatItem ConvertToChatItem(object dataItem, ChatItemConverterContext context)
    {
        return new TextMessage()
        {
            Data = dataItem,
            Author = ((MyChatMessage)dataItem).Author,
            Text = ((MyChatMessage)dataItem).Text
        };
    }
    
    public object ConvertToDataItem(object message, ChatItemConverterContext context)
    {
        var timestamp = DateTime.Now;
        var id = App.ChatService.GetLatestId() + 1;
        
        var chatMessage = new MyChatMessage(timestamp, id)
        {
            Author = App.ChatService.Me,
            Text = (string)message,
        };

        App.ChatService.UploadChatItem(chatMessage);
        
        return chatMessage;
    }
}

public class MyChatItemTemplateSelector : ChatItemTemplateSelector
{
    public DataTemplate LeftTemplate { get; set; }
    public DataTemplate RightTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is TextMessage tm)
        {
            Debug.WriteLine(tm.Author.Name);

            return tm.Author.Name == App.ChatService.Me.Name
                ? this.RightTemplate
                : this.LeftTemplate;
        }

        return base.OnSelectTemplate(item, container);
    }
}
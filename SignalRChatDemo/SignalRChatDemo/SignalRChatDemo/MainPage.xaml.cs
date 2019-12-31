using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using SignalRChatDemo.Services;
using Telerik.XamarinForms.ConversationalUI;
using Xamarin.Forms;

namespace SignalRChatDemo
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private ChatService service;
        private Author me = new Author { Name = "Xamarin.Forms User" };
        private ObservableCollection<Author> participants = new ObservableCollection<Author>();

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;

            ChatControl.Author = me;

            ((INotifyCollectionChanged)ChatControl.Items).CollectionChanged += ChatItems_CollectionChanged;
            MyTypingIndicator.Authors.CollectionChanged += TypingIndicatorAuthors_CollectionChanged;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // show the busy indicator while we're connecting
            BusyIndicator.IsVisible = true;
            BusyIndicator.IsBusy = true;

            // Create the SignalR service class
            service = new ChatService("https://uiforxamarinchatserver.azurewebsites.net/ChatHub");
            service.OnMessageReceived += ServiceOnMessageReceived;
            service.OnTypersUpdated += Service_OnTypersUpdated;

            // Start the service to open the connection
            await service.StartAsync();

            // Hide the busy indicator
            BusyIndicator.IsBusy = false;
            BusyIndicator.IsVisible = false;
        }

        private void Service_OnTypersUpdated(string username, bool isTyping)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                // no need to check if the current user was changed
                if (username == me.Name)
                {
                    return;
                }

                // Check if the author is already in the local list of chart participants
                Author author = participants.FirstOrDefault(a => a.Name == username);

                // If they haven't add them now and add them to the collection
                if (author == null)
                {
                    author = new Author
                    {
                        Name = username
                    };

                    if (!participants.Contains(author))
                        participants.Add(author);
                }

                if (isTyping) // add author to typers list
                {
                    if (!MyTypingIndicator.Authors.Contains(author))
                    {
                        MyTypingIndicator.Authors.Add(author);
                    }
                }
                else // remove author if they've stopped typing
                {
                    if (MyTypingIndicator.Authors.Contains(author))
                    {
                        MyTypingIndicator.Authors.Remove(author);
                    }
                }
            });
        }

        private void ServiceOnMessageReceived(string username, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                // If the incoming message is a duplicate of my outgoing message, don't add it to the chat control
                if (username == me.Name)
                {
                    return;
                }

                // Check if the author is already in the local list of chart participants
                Author author = participants.FirstOrDefault(a => a.Name == username);

                // If they haven't add them now and add them to the collection
                if (author == null)
                {
                    author = new Author
                    {
                        Name = username
                    };

                    if (!participants.Contains(author))
                        participants.Add(author);
                }

                // prepare the message object using the incoming information
                var textMessage = new TextMessage
                {
                    Author = author,
                    Data = message,
                    Text = message
                };

                // Add it to RadChat
                ChatControl.Items.Add(textMessage);
            });
        }

        private async void ChatItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var chatMessage = (TextMessage)e.NewItems[0];

                // The RadChat will add the types
                if (chatMessage.Author == ChatControl.Author)
                {
                    var authorName = chatMessage.Author.Name;
                    var messageToSend = chatMessage.Text;

                    await service.SendMessageAsync(authorName, messageToSend);
                }
            }
        }

        private async void TypingIndicatorAuthors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Contains(me))
            {
                await service.SendTyperAsync(me.Name, true);
            }

            if (e.OldItems != null && e.OldItems.Contains(me))
            {
                await service.SendTyperAsync(me.Name, false);
            }
        }

        private void RadEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                // If the text is not empty add the user to the current typers list
                if (!MyTypingIndicator.Authors.Contains(me))
                {
                    MyTypingIndicator.Authors.Add(me);
                }
            }
            else
            {
                // USer has sent message because text is empty, remove them from the typers
                if (MyTypingIndicator.Authors.Contains(me))
                {
                    MyTypingIndicator.Authors.Remove(me);
                }
            }
        }
    }
}

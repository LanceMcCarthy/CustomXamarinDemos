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
        private Author me = new Author {Name = "Xamarin.Forms User"};
        private ObservableCollection<Author> participants = new ObservableCollection<Author>();

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;

            ChatControl.Author = me;

            ((INotifyCollectionChanged)ChatControl.Items).CollectionChanged += ChatItems_CollectionChanged;
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

            // Start the service to open the connection
            await service.StartAsync();

            // Hide the busy indicator
            BusyIndicator.IsBusy = false;
            BusyIndicator.IsVisible = false;
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

                    if(!participants.Contains(author))
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
    }
}

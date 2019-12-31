using System;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRChatDemo.Services
{
    public class ChatService : INotifyPropertyChanged
    {
        private readonly HubConnection connection;
        public delegate void MessageReceived(string username, string message);
        public delegate void TypersReceived(string username, bool isTyping);
        public event MessageReceived OnMessageReceived;
        public event TypersReceived OnTypersUpdated;

        public ChatService(string url)
        {
            connection = new HubConnectionBuilder().WithUrl(url).Build();

            connection.Closed += async (error) =>
            {
                // Raise property changed
                OnPropertyChanged(nameof(ConnectionState));

                await Task.Delay(new Random().Next(0, 5) * 1000);

                await connection.StartAsync();
            };

            connection.On<string, string>("ReceiveMessage", (username, text) => 
            {
                OnMessageReceived?.Invoke(username, text);
            });

            connection.On<string, bool>("ReceiveTyper", (username, isTyping) =>
            {
                OnTypersUpdated?.Invoke(username, isTyping);
            });
        }

        public async Task SendMessageAsync(string username, string text)
        {
            await connection.InvokeAsync("SendMessage", username, text);
        }

        public async Task SendTyperAsync(string username, bool isTyping)
        {
            await connection.InvokeAsync("SendTyper", username, isTyping);
        }

        public Task StartAsync()
        {
            return connection.StartAsync();
        }

        public HubConnectionState ConnectionState => connection.State;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChatDemo.Webserver.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendTyper(string user, bool isTyping)
        {
            await Clients.All.SendAsync("ReceiveTyper", user, isTyping);
        }
    }
}

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RealTimeChatApp.Hubs
{
    // The Hub class coordinates incoming connections and broadcasts messages to groups
    public class ChatHub : Hub
    {
        // Broadcasts a text message payload to all active members inside a specific room
        public async Task SendMessage(string user, string message, string roomName)
        {
            await Clients.Group(roomName).SendAsync("ReceiveMessage", user, message);
        }

        // Connects a user session identity to an isolated room group channel
        public async Task JoinRoom(string user, string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

            // Broadcast a system-generated entry alert message to the group members
            await Clients.Group(roomName).SendAsync("ReceiveMessage", "System", $"{user} has joined the chat room: {roomName}.");
        }
    }
}
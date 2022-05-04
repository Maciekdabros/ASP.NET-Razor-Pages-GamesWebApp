using GamesWebApp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesWebApp.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }
        public async Task SendMessage(string user, string message)
        {
            //message send to all users
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    
        public Task SendMessageToGroup(string sender, string receiver, string message)
        {
            //message send to receiver only
            return Clients.Group(receiver).SendAsync("ReceiveMessage", sender, message);
        }
    }
}

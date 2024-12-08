
using Microsoft.AspNetCore.SignalR;

namespace Features.HubFolder
{
    public class UserHub: Hub
    {
        public async Task CreateUserNotification(string Username, string id){
            await Clients.All.SendAsync("User Created", Username,id);
        }
    }
}
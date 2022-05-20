using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs
{
    public class MyHub: Hub
    {
        // method that the client use this method when sending value and the server notify all clients about something
        public async Task Changed(string value)
        {
            // it invokes the func ChangeRecieved in the clients side
            await Clients.All.SendAsync("ChangeRecieved",value);
        }
    }
}

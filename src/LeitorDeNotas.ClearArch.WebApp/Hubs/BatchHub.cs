using Microsoft.AspNetCore.SignalR;

namespace LeitorDeNotas.ClearArch.WebApp.Hubs;

public class BatchHub : Hub
{
    public async Task NotifyProgress(string message)
    {
        await Clients.All.SendAsync("ReceiveProgress", message);
    }
}

using AppBackend.Application.ModelsDTO;
using Microsoft.AspNetCore.SignalR;

namespace AppBackend.Application.Common.Services
{
	public class NotificationHub : Hub
	{
		public async Task SendNotification(NotificationDTO data)
		{
			await Clients.All.SendAsync("activity", data);
		}
	}
}

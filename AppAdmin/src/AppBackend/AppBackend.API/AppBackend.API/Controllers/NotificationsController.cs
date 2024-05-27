using AppBackend.Application.Common.Services;
using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AppBackend.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class NotificationsController : ControllerBase
	{
		private readonly IHubContext<NotificationHub> _hubContext;
		private readonly INotificationService _notificationService;
		public NotificationsController(IHubContext<NotificationHub> hubContext, INotificationService notificationService)
		{
			_hubContext = hubContext;
			_notificationService = notificationService;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] NotificationModel notification)
		{
			await _hubContext.Clients.All.SendAsync("a", notification.User, notification.Message);
			return Ok("ok");
		}
		[HttpGet]
		public async Task<IActionResult> GetAll(int pageSize, int pageNum)
		{
			try
			{
				var result = await _notificationService.GetAllPage(pageSize, pageNum);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		[HttpGet]
		public async Task<IActionResult> GetCount()
		{
			try
			{
				var result = await _notificationService.GetCount();
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

	}
}

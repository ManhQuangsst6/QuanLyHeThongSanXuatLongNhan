using AppBackend.Application.Common.Mappings;
using AppBackend.Application.Common.Response;
using AppBackend.Application.Common.Services;
using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using AppBackend.Data.Context;
using AppBackend.Data.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AppBackend.Application.Services
{
	public class NotificationService : INotificationService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IHubContext<NotificationHub> _hubContext;
		public NotificationService(DataContext _dataContext, IMapper _mapper,
			IHttpContextAccessor httpContextAccessor, IHubContext<NotificationHub> hubContext)
		{
			this._dataContext = _dataContext;
			this._mapper = _mapper;
			this._httpContextAccessor = httpContextAccessor;
			_hubContext = hubContext;
		}
		public async Task<Response<PaginatedList<NotificationDTO>>> GetAllPage(int pageSize, int pageNum)
		{
			//var idUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var employees = _dataContext.Employees.AsNoTracking();
			var notifications = _dataContext.Notifications.AsNoTracking();
			//var query = from e in employees
			//			join n in notifications on e.Id equals n.CreatedBy
			//			orderby n.Created descending
			//			select new Notification() { Content = n.Content, Created = n.Created, CreatedBy = e.FullName };
			var query = from n in notifications
						orderby n.Created descending
						select n;
			var result = await query.ProjectTo<NotificationDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<NotificationDTO>> { IsSuccess = true, Status = 200, Value = result };

		}

		public async Task<Response<int>> GetCount()
		{
			var num = await _dataContext.Notifications.Where(x => x.IsRead == 0).OrderByDescending(x => x.Created).CountAsync();
			return new Response<int> { IsSuccess = true, Status = 200, Value = (num != null && num > 0) ? num : 0 };
		}

		public async Task<Response<string>> Post(NotificationDTO notificationDTO)
		{
			try
			{
				var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var user = await _dataContext.Employees.FirstOrDefaultAsync(x => x.Id == userID);
				notificationDTO.ID = Guid.NewGuid().ToString();
				notificationDTO.Created = DateTime.Now;
				notificationDTO.CreatedBy = user.EmployeeCode;
				notificationDTO.IsRead = 0;
				var notification = _mapper.Map<Notification>(notificationDTO);
				notification.IsRead = 0;
				await _dataContext.Notifications.AddAsync(notification);
				await _dataContext.SaveChangesAsync();
				notificationDTO.Image = user.ImageLink;
				await _hubContext.Clients.All.SendAsync("activity", notificationDTO);
				return new Response<string> { IsSuccess = true, Status = 200, Value = notificationDTO.ID };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Read(string id)
		{
			try
			{
				var notification = await _dataContext.Notifications.FirstOrDefaultAsync(x => x.Id == id);
				if (notification == null) return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found notification!" };

				notification.IsRead = 1;
				_dataContext.Notifications.Update(notification);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = notification.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Remove(string id)
		{
			try
			{
				var notifications = _dataContext.Notifications.Where(x => x.Id == id);
				if (await notifications.AnyAsync())
					return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found notifications " };
				_dataContext.Notifications.RemoveRange(notifications);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = "OK" };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}
	}
}

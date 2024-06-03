using AppBackend.Application.Common.ConvertData;
using AppBackend.Application.Common.Mappings;
using AppBackend.Application.Common.Response;
using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using AppBackend.Data.Context;
using AppBackend.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using static AppBackend.Data.Enums.EnumData;

namespace AppBackend.Application.Services
{
	public class RegisterRemainningLonganService : IRegisterRemainningLonganService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly INotificationService _notificationService;
		public RegisterRemainningLonganService(DataContext dataContext, IMapper mapper,
			IHttpContextAccessor httpContextAccessor, INotificationService notificationService)
		{
			_dataContext = dataContext;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
			_notificationService = notificationService;
		}

		public async Task<Response<RegisterRemainningLonganDTO>> Get(string id)
		{
			var result = await _dataContext.RegisterRemainningLongans.FirstOrDefaultAsync(x => x.Id == id);
			if (result == null) return new Response<RegisterRemainningLonganDTO>() { IsSuccess = false, Status = 404, Message = "Not Found RegisterDayLongan!" };
			return new Response<RegisterRemainningLonganDTO>() { IsSuccess = true, Status = 200, Value = _mapper.Map<RegisterRemainningLonganDTO>(result) };
		}

		public async Task<Response<PaginatedList<RegisterRemainningLonganDTO>>> GetAllPage(int pageSize, int pageNum, string? searchName, DateTimeOffset? currentDate)
		{
			if (currentDate == null) currentDate = DateTimeOffset.Now.AddHours(7);
			var employees = _dataContext.Employees.AsNoTracking();
			var registerDayLongans = _dataContext.RegisterRemainningLongans.AsNoTracking();
			var query = from e in employees
						join r in registerDayLongans on e.Id equals r.EmployeeID
						where (r.Created.Value.Year == currentDate.Value.Year && r.Created.Value.Month == currentDate.Value.Month &&
						 r.Created.Value.Day == currentDate.Value.Day && e.IsDeleted == 0 && e.Status == (int)StatusEmployee.Active)
						 && (e.EmployeeCode.Contains(searchName) || searchName.IsNullOrEmpty())
						orderby r.Created, e.EmployeeCode
						select new RegisterRemainningLonganDTO
						{
							Id = r.Id,
							EmployeeID = r.EmployeeID,
							EmployeeCode = e.EmployeeCode,
							EmployeeName = e.FullName,
							Amount = r.Amount,
							Ischeck = r.Ischeck,
							Created = r.Created,
							Status = ConvertData.ConvertStatusRegisterLongan(r.Ischeck)
						};

			var result = await query.PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<RegisterRemainningLonganDTO>> { IsSuccess = true, Status = 200, Value = result };
		}
		public async Task<Response<PaginatedList<RegisterRemainningLonganDTO>>> GetAllPageUser(int pageSize, int pageNum, int? status, DateTimeOffset? currentDate)
		{
			var httpContext = _httpContextAccessor.HttpContext;
			var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (currentDate == null) currentDate = DateTimeOffset.Now.AddHours(7);
			var employees = _dataContext.Employees.AsNoTracking();
			var registerDayLongans = _dataContext.RegisterRemainningLongans.AsNoTracking();
			var query = from e in employees
						join r in registerDayLongans on e.Id equals r.EmployeeID
						where (r.Created.Value.Year == currentDate.Value.Year && r.Created.Value.Month == currentDate.Value.Month &&
						 r.Created.Value.Day == currentDate.Value.Day && e.IsDeleted == 0 && e.Status == (int)StatusEmployee.Active)
						 && e.Id == userId && (status == null || r.Ischeck == status)
						orderby r.Created descending, r.Ischeck descending
						select new RegisterRemainningLonganDTO
						{
							Id = r.Id,
							EmployeeID = r.EmployeeID,
							EmployeeCode = e.EmployeeCode,
							EmployeeName = e.FullName,
							Amount = r.Amount,
							Ischeck = r.Ischeck,
							Created = r.Created,
							Status = ConvertData.ConvertStatusRegisterLongan(r.Ischeck)
						};

			var result = await query.PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<RegisterRemainningLonganDTO>> { IsSuccess = true, Status = 200, Value = result };
		}

		public async Task<Response<string>> Post(RegisterRemainningLonganDTO registerRemainningLonganDTO)
		{

			try
			{
				var httpContext = _httpContextAccessor.HttpContext;
				var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				registerRemainningLonganDTO.Id = Guid.NewGuid().ToString();
				registerRemainningLonganDTO.EmployeeID = userId;
				var registerRemainningLongan = _mapper.Map<RegisterRemainningLongan>(registerRemainningLonganDTO);
				registerRemainningLongan.Created = DateTimeOffset.Now;
				await _dataContext.RegisterRemainningLongans.AddAsync(registerRemainningLongan);
				await _dataContext.SaveChangesAsync();
				var notification = new NotificationDTO() { Link = "register-remainning-longan", Content = " đã đăng kí trả nhãn " };
				await _notificationService.Post(notification);
				return new Response<string> { IsSuccess = true, Status = 200, Value = registerRemainningLongan.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Put(RegisterRemainningLonganDTO registerRemainningLonganDTO)
		{
			try
			{
				var registerRemainningLongan = await _dataContext.RegisterRemainningLongans.FirstOrDefaultAsync(x => x.Id == registerRemainningLonganDTO.Id);
				if (registerRemainningLongan == null) return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found registerDayLongan!" };
				registerRemainningLongan.Ischeck = (int)registerRemainningLonganDTO.Ischeck;
				_dataContext.RegisterRemainningLongans.Update(registerRemainningLongan);
				var status = registerRemainningLonganDTO.Ischeck;
				var message = "";
				if (status == 1) message = "đang đến lấy nhãn";
				else if (status == 2) message = "Đã trả nhãn cho nhân viên";
				else if (status == 3) message = "Đã hủy đăng kí trả nhãn";
				var notification = new NotificationDTO() { Content = message };
				await _notificationService.Post(notification);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = registerRemainningLongan.Id };
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
				var registerDayLongan = await _dataContext.RegisterRemainningLongans.FirstOrDefaultAsync(x => x.Id == id);
				if (registerDayLongan == null)
					return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found registerDayLongan " };
				_dataContext.RegisterRemainningLongans.Remove(registerDayLongan);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

	}
}

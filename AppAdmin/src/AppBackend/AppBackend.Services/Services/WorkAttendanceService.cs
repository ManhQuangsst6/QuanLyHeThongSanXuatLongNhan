using AppBackend.Application.Common.Mappings;
using AppBackend.Application.Common.Response;
using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using AppBackend.Data.Context;
using AppBackend.Data.Models;
using AppBackend.Data.Models.Email;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using static AppBackend.Data.Enums.EnumData;

namespace AppBackend.Application.Services
{
	public class WorkAttendanceService : IWorkAttendanceService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IEmailService _emailService;
		public WorkAttendanceService(DataContext dataContext, IMapper mapper, RoleManager<IdentityRole> roleManager,
			IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, IEmailService emailService)
		{
			_dataContext = dataContext;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
			_roleManager = roleManager;
			_emailService = emailService;
		}


		public async Task<Response<string>> ComfirmByEmployee(string id)
		{
			try
			{
				var workAttendance = await _dataContext.WorkAttendances.FirstOrDefaultAsync(x => x.Id == id);
				if (workAttendance == null) return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found workAttendance!" };
				workAttendance.ComfirmAmount = (int)ComfirmWorkAmount.Fail;
				_dataContext.WorkAttendances.Update(workAttendance);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = workAttendance.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}
		public async Task<Response<string>> ComfirmAuto()
		{
			try
			{
				var workAttendances = await _dataContext.WorkAttendances.Where(x => x.Created.Value.Date == DateTimeOffset.Now.Date).ToListAsync();
				if (workAttendances == null) return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found workAttendance!" };
				foreach (var workAttendance in workAttendances)
				{
					workAttendance.ComfirmAmount = (int)ComfirmWorkAmount.Success;
					_dataContext.WorkAttendances.Update(workAttendance);
				}
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = workAttendances[0].Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<WorkAttendanceDTO>> Get(string id)
		{
			var result = await _dataContext.WorkAttendances.FirstOrDefaultAsync(x => x.Id == id);
			if (result == null) return new Response<WorkAttendanceDTO>() { IsSuccess = false, Status = 404, Message = "Not Found WorkAttendance!" };
			return new Response<WorkAttendanceDTO>() { IsSuccess = true, Status = 200, Value = _mapper.Map<WorkAttendanceDTO>(result) };
		}

		public async Task<Response<PaginatedList<WorkAttendanceDTO>>> GetAllByEmployee(int pageSize, int pageNum, DateTimeOffset? dateTime)
		{
			var httpContext = _httpContextAccessor.HttpContext;
			var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var employees = _dataContext.Employees.AsNoTracking();
			var workAttendances = _dataContext.WorkAttendances.AsNoTracking();
			var currentDate = dateTime.HasValue ? dateTime.Value : DateTimeOffset.Now;
			var query = from e in employees
						join w in workAttendances on e.Id equals w.EmployeeID
						where (w.Created.Value.Year == currentDate.Year && w.Created.Value.Month == currentDate.Month &&
						 w.Created.Value.Day == currentDate.Day && e.IsDeleted == 0 && e.Status == (int)StatusEmployee.Active)
						 && (e.Id == userId)
						orderby w.Created
						select w;

			var result = await query.ProjectTo<WorkAttendanceDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<WorkAttendanceDTO>> { IsSuccess = true, Status = 200, Value = result };
		}

		public async Task<Response<PaginatedList<WorkAttendanceDTO>>> GetAllPage(int pageSize, int pageNum, string? nameSearch, DateTimeOffset? dateTime)
		{
			var employees = _dataContext.Employees.AsNoTracking();
			var workAttendances = _dataContext.WorkAttendances.AsNoTracking();
			var currentDate = dateTime.HasValue ? dateTime : DateTimeOffset.Now;
			var query = from e in employees
						join w in workAttendances on e.Id equals w.EmployeeID
						where (w.Created.Value.Year == currentDate.Value.Year && w.Created.Value.Month == currentDate.Value.Month &&
						 w.Created.Value.Day == currentDate.Value.Day && e.IsDeleted == 0 && e.Status == (int)StatusEmployee.Active)
						 && (e.EmployeeCode == nameSearch || nameSearch.IsNullOrEmpty())
						orderby w.Created, e.EmployeeCode
						select w;

			var result = await query.ProjectTo<WorkAttendanceDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<WorkAttendanceDTO>> { IsSuccess = true, Status = 200, Value = result };
		}

		public async Task<Response<string>> Post()
		{
			try
			{
				var date = DateTimeOffset.Now;
				var check = await _dataContext.WorkAttendances.Where(x => x.Created.Value.Day == date.Day &&
				x.Created.Value.Month == date.Month && x.Created.Value.Year == date.Year).AnyAsync();
				if (check) throw new Exception("Đã tạo rồi nhé");
				var list = new List<WorkAttendance>();
				var employee = await _dataContext.Employees.Where(x => x.Status == (int)StatusEmployee.Active)
					.Select(x => new WorkAttendanceDTO { Id = Guid.NewGuid().ToString(), EmployeeID = x.Id, Created = DateTimeOffset.Now })
					.ToListAsync();
				foreach (var e in employee)
				{
					list.Add(_mapper.Map<WorkAttendance>(e));
				}
				await _dataContext.WorkAttendances.AddRangeAsync(list);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200 };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Put(WorkAttendanceDTO workAttendanceDTO)
		{
			try
			{
				var workAttendance = await _dataContext.WorkAttendances.FirstOrDefaultAsync(x => x.Id == workAttendanceDTO.Id);
				if (workAttendance == null) return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found workAttendance!" };

				workAttendance.ListAmount = workAttendanceDTO.ListAmount;
				workAttendance.SumAmount = workAttendanceDTO.SumAmount;
				_dataContext.WorkAttendances.Update(workAttendance);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = workAttendance.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Remove()
		{
			try
			{
				var workAttendance = _dataContext.WorkAttendances.Where(x => x.SumAmount == 0);
				if (await workAttendance.AnyAsync())
					return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found workAttendance " };
				_dataContext.WorkAttendances.RemoveRange(workAttendance);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = "OK" };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task SendMailToEmployee()
		{
			try
			{
				var users = await _userManager.GetUsersInRoleAsync("User");
				var workDay = await _dataContext.WorkAttendances.Where(x => users.Select(a => a.Id).Contains(x.EmployeeID)).ToListAsync();
				var employeeUsers = users.Select(u => new
				{
					u.Id,
					u.UserName,
					u.Email,
					u.PhoneNumber,
					workDay.FirstOrDefault(x => x.EmployeeID == u.Id).SumAmount
				}).ToList();
				foreach (var user in employeeUsers)
				{
					if (!user.Email.IsNullOrEmpty() && user.SumAmount != null && user.SumAmount > 0)
						SendMail(user.Email, (int)user.SumAmount);
					if (!user.PhoneNumber.IsNullOrEmpty() && user.SumAmount != null && user.SumAmount > 0)
						if (user.PhoneNumber == "0357021457")
							SendPhone(user.PhoneNumber, (int)user.SumAmount);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		private async void SendMail(string email, int count)
		{
			var date = DateTime.Now.ToString("dd/MM/yyyy");

			var html = @"
<html lang=""en"">
    <head>    
        <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
        <title>
            Upcoming topics
        </title>
        <style type=""text/css"">
            HTML{background-color: #e8e8e8;}
            .courses-table{font-size: 12px; padding: 3px; border-collapse: collapse; border-spacing: 0;}
            .courses-table .description{color: #505050;}
            .courses-table td{border: 1px solid #D1D1D1; background-color: #F3F3F3; padding: 0 10px;}
            .courses-table th{border: 1px solid #424242; color: #FFFFFF;text-align: left; padding: 0 10px;}
            .green{background-color: #6B9852;}
        </style>
    </head>
    <body>
        <table class=""courses-table"">
            <thead>
                <tr>
                    <th class=""green"">Ngày</th>
                    <th class=""green"">Số cân</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class=""description"">" + date + @"</td>
                    <td>" + count + @"</td>
                </tr>
               
            </tbody>
        </table>
<div>Nếu có sai sót hãy xác nhận lại trước 11h đêm ngày  " + date + @"</div>
    </body>
</html>
";
			Message message = new Message(new string[] { email }, "Thông báo số cân ngày " + date, html);
			await _emailService.SendEmail(message);
		}
		private async void SendPhone(string phoneNumber, int count)
		{
			var date = DateTime.Now.ToString("dd/MM/yyyy");
			var accountSid = "";
			var authToken = "";
			TwilioClient.Init(accountSid, authToken);

			var messageOptions = new CreateMessageOptions(
			  new PhoneNumber("+84" + phoneNumber.Substring(1)));
			messageOptions.From = new PhoneNumber("+13345186173");
			messageOptions.Body = "Số cân nhãn đã làm ngày hôm nay(" + date + ") là : " + count;

			var message = MessageResource.Create(messageOptions);
			Console.WriteLine(message.Body);
		}
	}
}

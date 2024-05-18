﻿using AppBackend.Application.Common.Mappings;
using AppBackend.Application.Common.Response;
using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using AppBackend.Data.Context;
using AppBackend.Data.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using static AppBackend.Data.Enums.EnumData;

namespace AppBackend.Application.Services
{
	public class WorkAttendanceService : IWorkAttendanceService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public WorkAttendanceService(DataContext dataContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			_dataContext = dataContext;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
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
			var currentDate = dateTime.HasValue ? dateTime.Value.AddHours(7) : DateTimeOffset.Now;
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
	}
}
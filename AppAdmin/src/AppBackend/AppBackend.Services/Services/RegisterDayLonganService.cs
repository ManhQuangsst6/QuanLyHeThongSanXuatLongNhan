﻿using AppBackend.Application.Common.ConvertData;
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
	public class RegisterDayLonganService : IRegisterDayLonganService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public RegisterDayLonganService(DataContext dataContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			_dataContext = dataContext;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<Response<RegisterDayLonganDTO>> Get(string id)
		{
			var result = await _dataContext.RegisterDayLongans.FirstOrDefaultAsync(x => x.Id == id);
			if (result == null) return new Response<RegisterDayLonganDTO>() { IsSuccess = false, Status = 404, Message = "Not Found RegisterDayLongan!" };
			return new Response<RegisterDayLonganDTO>() { IsSuccess = true, Status = 200, Value = _mapper.Map<RegisterDayLonganDTO>(result) };
		}

		public async Task<Response<PaginatedList<RegisterDayLonganDTO>>> GetAllPage(int pageSize, int pageNum, string? searchName, DateTimeOffset currentDate)
		{
			var employees = _dataContext.Employees.AsNoTracking();
			currentDate = currentDate.AddHours(7);
			var registerDayLongans = _dataContext.RegisterDayLongans.AsNoTracking();
			var query = from e in employees
						join r in registerDayLongans on e.Id equals r.EmployeeID
						where (r.Created.Value.Year == currentDate.Year && r.Created.Value.Month == currentDate.Month &&
						 r.Created.Value.Day == currentDate.Day && e.IsDeleted == 0 && e.Status == (int)StatusEmployee.Active)
						 && (e.EmployeeCode.Contains(searchName) || searchName.IsNullOrEmpty())
						orderby e.EmployeeCode
						select new RegisterDayLonganDTO
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
			return new Response<PaginatedList<RegisterDayLonganDTO>> { IsSuccess = true, Status = 200, Value = result };
		}

		public async Task<Response<string>> Post(RegisterDayLonganDTO registerDayLonganDTO)
		{
			try
			{
				var httpContext = _httpContextAccessor.HttpContext;
				var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				registerDayLonganDTO.Id = Guid.NewGuid().ToString();
				registerDayLonganDTO.EmployeeID = userId;
				var registerDayLongan = _mapper.Map<RegisterDayLongan>(registerDayLonganDTO);
				registerDayLongan.Created = DateTimeOffset.Now.AddHours(7);
				await _dataContext.RegisterDayLongans.AddAsync(registerDayLongan);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = registerDayLonganDTO.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Put(RegisterDayLonganDTO registerDayLonganDTO)
		{
			try
			{
				var registerDayLongan = await _dataContext.RegisterDayLongans.FirstOrDefaultAsync(x => x.Id == registerDayLonganDTO.Id);
				if (registerDayLongan == null) return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found registerDayLongan!" };
				registerDayLongan.Amount = registerDayLonganDTO.Amount;
				registerDayLongan.Ischeck = (int)registerDayLonganDTO.Ischeck;
				_dataContext.RegisterDayLongans.Update(registerDayLongan);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = registerDayLongan.Id };
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
				var registerDayLongan = await _dataContext.RegisterDayLongans.FirstOrDefaultAsync(x => x.Id == id);
				if (registerDayLongan == null)
					return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found registerDayLongan " };
				_dataContext.RegisterDayLongans.Remove(registerDayLongan);
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

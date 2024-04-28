using AppBackend.Application.Common.Mappings;
using AppBackend.Application.Common.Response;
using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using AppBackend.Data.Context;
using AppBackend.Data.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static AppBackend.Data.Enums.EnumData;

namespace AppBackend.Application.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		public EmployeeService(DataContext dataContext, IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public async Task<Response<EmployeeDTO>> Get(string id)
		{
			var result = await _dataContext.Employees.FirstOrDefaultAsync(o => o.Id == id);
			return new Response<EmployeeDTO>() { IsSuccess = true, Status = 200, Value = _mapper.Map<EmployeeDTO>(result) };
		}

		public async Task<Response<List<EmployeeDTO>>> GetAllEmployee()
		{
			var employees = _dataContext.Employees.AsNoTracking();
			var userRole = _dataContext.UserRoles.AsNoTracking();
			var role = _dataContext.Roles.AsNoTracking();
			var query = from e in employees
						join ur in userRole on e.Id equals ur.UserId
						join r in role on ur.RoleId equals r.Id
						where r.Name == "Employee"
						select e;
			var result = await query.ProjectToListAsync<EmployeeDTO>(_mapper.ConfigurationProvider);
			return new Response<List<EmployeeDTO>>() { IsSuccess = true, Status = 200, Value = result };
		}

		public async Task<Response<PaginatedList<EmployeeDTO>>> GetAllPage(int pageSize = 10, int pageNum = 1)
		{
			var query = _dataContext.Employees.AsNoTracking();
			var objs = await query.ProjectTo<EmployeeDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<EmployeeDTO>> { IsSuccess = true, Status = 200, Value = objs };
		}

		public async Task<Response<PaginatedList<EmployeeDTO>>> GetAllUserPage(string? nameSearch, int pageSize = 10, int pageNum = 1)
		{
			var employees = _dataContext.Employees.AsNoTracking();
			var userRole = _dataContext.UserRoles.AsNoTracking();
			var role = _dataContext.Roles.AsNoTracking();
			var query = from e in employees
						join ur in userRole on e.Id equals ur.UserId
						join r in role on ur.RoleId equals r.Id
						where r.Name == "User" && ((e.EmployeeCode.Contains(nameSearch) || nameSearch.IsNullOrEmpty()))
						select e;


			var objs = await query.ProjectTo<EmployeeDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<EmployeeDTO>> { IsSuccess = true, Status = 200, Value = objs };
		}

		public async Task<Response<string>> Post(EmployeeDTO employeeDTO)
		{
			try
			{
				//employeeDTO.Id = Guid.NewGuid().ToString();

				var employee = _mapper.Map<Employee>(employeeDTO);
				employee.IsDeleted = (int)StatusIsDelete.Doing;
				await _dataContext.Employees.AddAsync(employee);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = employeeDTO.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Put(EmployeeDTO employeeDTO)
		{
			try
			{
				var employee = await _dataContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeDTO.Id);
				if (employee == null) return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found employee!" };
				employee.FullName = employeeDTO.FullName;
				employee.ImageLink = employeeDTO.ImageLink;
				//	employee.PositionID = employeeDTO.PositionID;
				employee.Status = employeeDTO.Status;

				_dataContext.Employees.Update(employee);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = employee.Id };
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
				var employee = await _dataContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
				if (employee == null)
					return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found employee " };
				employee.IsDeleted = (int)StatusIsDelete.Done;
				_dataContext.Employees.Update(employee);
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

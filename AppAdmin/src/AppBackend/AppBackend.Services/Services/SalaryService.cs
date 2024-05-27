using AppBackend.Application.Common.Mappings;
using AppBackend.Application.Common.Response;
using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using AppBackend.Data.Context;
using AppBackend.Data.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static AppBackend.Data.Enums.EnumData;

namespace AppBackend.Application.Services
{
	public class SalaryService : ISalaryService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<IdentityUser> _userManager;

		public SalaryService(DataContext dataContext, IMapper mapper,
			RoleManager<IdentityRole> role, UserManager<IdentityUser> userManager)
		{
			_dataContext = dataContext;
			_mapper = mapper;
			_roleManager = role;
			_userManager = userManager;
		}

		public async Task CreateTableSalary(int quarterYear, int year, int price)
		{
			var usersInRole = await _userManager.GetUsersInRoleAsync("User");
			var userIds = usersInRole.Select(u => u.Id).ToList();
			var employee = _dataContext.Employees.AsNoTracking();
			var workAttendances = _dataContext.WorkAttendances.AsNoTracking();
			var result = from e in employee
						 join w in workAttendances on e.Id equals w.EmployeeID
						 where w.Created.Value.Year == year && userIds.Contains(e.Id) && e.Status == (int)StatusEmployee.Active
						 && (w.Created.Value.Month > 3 * quarterYear - 3) && (w.Created.Value.Month < 3 * quarterYear + 1)
						 group w by e.Id into g
						 select new Salary
						 {
							 Id = Guid.NewGuid().ToString(),
							 EmployeeID = g.Key,
							 SalaryMoney = g.Sum(w => (decimal)w.SumAmount) * price,
							 SumAmount = g.Sum(w => (int)w.SumAmount),
							 QuarterYear = quarterYear,
							 Year = year,
							 Status = 0,
							 Created = DateTime.Now,
						 };
			var old = _dataContext.Salaries.Where(x => x.QuarterYear == quarterYear && x.Year == year && x.Status == 0);
			if (await old.AnyAsync())
				_dataContext.Salaries.RemoveRange(old);
			await _dataContext.Salaries.AddRangeAsync(result);
			await _dataContext.SaveChangesAsync();
		}

		public async Task<Response<SalaryDTO>> Get(string id)
		{
			var result = await _dataContext.Salaries.FirstOrDefaultAsync(x => x.Id == id);
			if (result == null) return new Response<SalaryDTO>() { IsSuccess = false, Status = 404, Message = "Not Found Salary!" };
			return new Response<SalaryDTO>() { IsSuccess = true, Status = 200, Value = _mapper.Map<SalaryDTO>(result) };
		}

		public async Task<Response<PaginatedList<SalaryDTO>>> GetAllByEmployee(int pageSize, int pageNum, string? employeeCode)
		{
			var employee = _dataContext.Employees.AsNoTracking();
			var salary = _dataContext.Salaries.AsNoTracking();
			var query = from e in employee
						join s in salary on e.Id equals s.EmployeeID
						where e.EmployeeCode == employeeCode || employeeCode.IsNullOrEmpty()
						orderby s.Year descending, s.QuarterYear descending
						select s;

			var objs = await query.ProjectTo<SalaryDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<SalaryDTO>> { IsSuccess = true, Status = 200, Value = objs };
		}

		public async Task<Response<PaginatedList<SalaryDTO>>> GetAllPage(int pageSize, int pageNum, string? searchName, int? quarterYear, int? year)
		{
			var usersInRole = await _userManager.GetUsersInRoleAsync("User");
			var userIds = usersInRole.Select(u => u.Id).ToList();
			var employee = _dataContext.Employees.AsNoTracking();
			var salary = _dataContext.Salaries.AsNoTracking();
			var query = from e in employee
						join s in salary on e.Id equals s.EmployeeID
						where (e.EmployeeCode == searchName || searchName.IsNullOrEmpty())
						&& (s.Year == year || year == null) && (s.QuarterYear == quarterYear || quarterYear == null) && userIds.Contains(e.Id)
						orderby s.Year descending, s.QuarterYear descending
						select s;

			var objs = await query.ProjectTo<SalaryDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<SalaryDTO>> { IsSuccess = true, Status = 200, Value = objs };
		}

		public async Task<Response<List<SalaryDTO>>> GetAllExportExcel(int? quarterYear, int? year)
		{
			var usersInRole = await _userManager.GetUsersInRoleAsync("User");
			var userIds = usersInRole.Select(u => u.Id).ToList();
			var employee = _dataContext.Employees.AsNoTracking();
			var salary = _dataContext.Salaries.AsNoTracking();
			var query = from e in employee
						join s in salary on e.Id equals s.EmployeeID
						where
						 (s.Year == year || year == null) && (s.QuarterYear == quarterYear || quarterYear == null)
						 && userIds.Contains(e.Id)
						select s;
			//	var objs = await query.ToListAsync();
			var value = query.ProjectTo<SalaryDTO>(_mapper.ConfigurationProvider);
			return new Response<List<SalaryDTO>> { IsSuccess = true, Status = 200, Value = await value.ToListAsync() };
		}

		public async Task<Response<PaginatedList<SalaryDTO>>> GetTableSalary(int pageSize, int pageNum, int? quarterYear, int? year)
		{
			var usersInRole = await _userManager.GetUsersInRoleAsync("User");
			var userIds = usersInRole.Select(u => u.Id).ToList();
			var employee = _dataContext.Employees.AsNoTracking();
			var salary = _dataContext.Salaries.AsNoTracking();
			var query = from e in employee
						join s in salary on e.Id equals s.EmployeeID
						where s.QuarterYear == quarterYear && s.Year == year && userIds.Contains(e.Id)
						orderby s.Year descending, s.QuarterYear descending
						select s;

			var objs = await query.ProjectTo<SalaryDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<SalaryDTO>> { IsSuccess = true, Status = 200, Value = objs };
		}

		public async Task<Response<string>> Post(SalaryDTO salaryDTO)
		{
			try
			{
				salaryDTO.Id = Guid.NewGuid().ToString();
				var salary = _mapper.Map<Salary>(salaryDTO);
				await _dataContext.Salaries.AddAsync(salary);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = salaryDTO.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Put(SalaryDTO salaryDTO)
		{
			try
			{
				var salary = _dataContext.Salaries.FirstOrDefault(x => x.Id == salaryDTO.Id);
				if (salary == null)
				{
					throw new Exception("Salary not found");
				}
				salary.Status = 1; await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200 };
			}
			catch (Exception ex)
			{
				return new Response<string> { Message = ex.Message, IsSuccess = false, Status = 404 };
			}
		}

		public async Task<Response<string>> Remove(string id)
		{
			try
			{
				var salary = await _dataContext.Salaries.FirstOrDefaultAsync(x => x.Id == id);
				if (salary == null)
					return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found salary " };
				_dataContext.Salaries.Update(salary);
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

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

namespace AppBackend.Application.Services
{
	public class SalaryService : ISalaryService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		public SalaryService(DataContext dataContext, IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public async Task CreateTableSalary(int quarterYear, int year, int price)
		{
			var employee = _dataContext.Employees.AsNoTracking();
			var workAttendances = _dataContext.WorkAttendances.AsNoTracking();
			var result = from e in employee
						 join w in workAttendances on e.Id equals w.EmployeeID
						 where w.Created.Value.Year == year
						 && (w.Created.Value.Month > 3 * quarterYear - 3) && (w.Created.Value.Month < 3 * quarterYear + 1)
						 group w by e.Id into g
						 select new Salary
						 {
							 Id = Guid.NewGuid().ToString(),
							 EmployeeID = g.Key,
							 SalaryMoney = g.Sum(w => (decimal)w.SumAmount) * price,
							 QuarterYear = quarterYear,
							 Year = year,
							 Status = 0,
							 Created = DateTime.Now,
						 };
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
						orderby s.Year, s.QuarterYear
						select s;

			var objs = await query.ProjectTo<SalaryDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<SalaryDTO>> { IsSuccess = true, Status = 200, Value = objs };
		}

		public async Task<Response<PaginatedList<SalaryDTO>>> GetAllPage(int pageSize, int pageNum, string? searchName, int? quarterYear, int? year)
		{
			var employee = _dataContext.Employees.AsNoTracking();
			var salary = _dataContext.Salaries.AsNoTracking();
			var query = from e in employee
						join s in salary on e.Id equals s.EmployeeID
						where (e.EmployeeCode == searchName || searchName.IsNullOrEmpty())
						&& (s.Year == year || year == null) && (s.QuarterYear == quarterYear || quarterYear == null)
						orderby s.Year, s.QuarterYear
						select s;

			var objs = await query.ProjectTo<SalaryDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<SalaryDTO>> { IsSuccess = true, Status = 200, Value = objs };
		}

		public async Task<Response<PaginatedList<SalaryDTO>>> GetTableSalary(int pageSize, int pageNum, int? quarterYear, int? year)
		{
			var employee = _dataContext.Employees.AsNoTracking();
			var salary = _dataContext.Salaries.AsNoTracking();
			var query = from e in employee
						join s in salary on e.Id equals s.EmployeeID
						where s.QuarterYear == quarterYear && s.Year == year
						orderby s.Year, s.QuarterYear
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
			throw new NotImplementedException();
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

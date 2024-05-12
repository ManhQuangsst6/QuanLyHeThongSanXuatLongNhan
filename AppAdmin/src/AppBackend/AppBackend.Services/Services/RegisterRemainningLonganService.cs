using AppBackend.Application.Common.Mappings;
using AppBackend.Application.Common.Response;
using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using AppBackend.Data.Context;
using AppBackend.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static AppBackend.Data.Enums.EnumData;

namespace AppBackend.Application.Services
{
	public class RegisterRemainningLonganService : IRegisterRemainningLonganService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		public RegisterRemainningLonganService(DataContext dataContext, IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public async Task<Response<RegisterRemainningLonganDTO>> Get(string id)
		{
			var result = await _dataContext.RegisterRemainningLongans.FirstOrDefaultAsync(x => x.Id == id);
			if (result == null) return new Response<RegisterRemainningLonganDTO>() { IsSuccess = false, Status = 404, Message = "Not Found RegisterDayLongan!" };
			return new Response<RegisterRemainningLonganDTO>() { IsSuccess = true, Status = 200, Value = _mapper.Map<RegisterRemainningLonganDTO>(result) };
		}

		public async Task<Response<PaginatedList<RegisterRemainningLonganDTO>>> GetAllPage(int pageSize, int pageNum, string? searchName, DateTimeOffset currentDate)
		{
			var employees = _dataContext.Employees.AsNoTracking();
			var registerDayLongans = _dataContext.RegisterRemainningLongans.AsNoTracking();
			var query = from e in employees
						join r in registerDayLongans on e.Id equals r.EmployeeID
						where (r.Created.Value.Year == currentDate.Year && r.Created.Value.Month == currentDate.Month &&
						 r.Created.Value.Day == currentDate.Day && e.IsDeleted == 0 && e.Status == (int)StatusEmployee.Active)
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
						};

			var result = await query.PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<RegisterRemainningLonganDTO>> { IsSuccess = true, Status = 200, Value = result };
		}

		public async Task<Response<string>> Post(RegisterRemainningLonganDTO registerRemainningLonganDTO)
		{

			try
			{
				registerRemainningLonganDTO.Id = Guid.NewGuid().ToString();
				var registerRemainningLongan = _mapper.Map<RegisterRemainningLongan>(registerRemainningLonganDTO);
				registerRemainningLongan.Created = DateTimeOffset.UtcNow;
				await _dataContext.RegisterRemainningLongans.AddAsync(registerRemainningLongan);
				await _dataContext.SaveChangesAsync();
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
				registerRemainningLongan.Amount = registerRemainningLonganDTO.Amount;
				registerRemainningLongan.Ischeck = registerRemainningLonganDTO.Ischeck;
				_dataContext.RegisterRemainningLongans.Update(registerRemainningLongan);
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

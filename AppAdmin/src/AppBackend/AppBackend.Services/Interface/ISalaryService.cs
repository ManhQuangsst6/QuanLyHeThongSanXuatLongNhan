using AppBackend.Application.Common.Response;
using AppBackend.Application.ModelsDTO;

namespace AppBackend.Application.Interface
{
	public interface ISalaryService
	{
		Task<Response<string>> Post(SalaryDTO salaryDTO);
		Task<Response<string>> Put(SalaryDTO salaryDTO);
		Task<Response<string>> Remove(string id);
		Task<Response<SalaryDTO>> Get(string id);
		Task<Response<PaginatedList<SalaryDTO>>> GetAllPage(int pageSize, int pageNum, string? searchName, int? quarterYear, int? year);
		Task<Response<PaginatedList<SalaryDTO>>> GetAllByEmployee(int pageSize, int pageNum, string? employeeCode);
		Task CreateTableSalary(int quarterYear, int year, int price);
		Task<Response<PaginatedList<SalaryDTO>>> GetTableSalary(int pageSize, int pageNum, int? quarterYear, int? year);
	}
}

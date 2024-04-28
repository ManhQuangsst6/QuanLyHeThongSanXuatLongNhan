using AppBackend.Application.Common.Response;
using AppBackend.Application.ModelsDTO;

namespace AppBackend.Application.Interface
{
	public interface IEmployeeService
	{
		Task<Response<string>> Post(EmployeeDTO employeeDTO);
		Task<Response<string>> Put(EmployeeDTO employeeDTO);
		Task<Response<string>> Remove(string id);
		Task<Response<EmployeeDTO>> Get(string id);
		Task<Response<PaginatedList<EmployeeDTO>>> GetAllPage(int pageSize, int pageNum);
		Task<Response<List<EmployeeDTO>>> GetAllEmployee();
		Task<Response<PaginatedList<EmployeeDTO>>> GetAllUserPage(string nameSearch, int pageSize, int pageNum);

	}
}

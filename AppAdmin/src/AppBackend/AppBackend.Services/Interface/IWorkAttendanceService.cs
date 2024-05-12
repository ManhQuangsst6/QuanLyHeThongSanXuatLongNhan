using AppBackend.Application.Common.Response;
using AppBackend.Application.ModelsDTO;

namespace AppBackend.Application.Interface
{
	public interface IWorkAttendanceService
	{
		Task<Response<string>> Post();
		Task<Response<string>> Put(WorkAttendanceDTO workAttendanceDTO);
		Task<Response<string>> Remove(string id);
		Task<Response<WorkAttendanceDTO>> Get(string id);
		Task<Response<string>> ComfirmByEmployee(string id, string employeeID);
		Task<Response<PaginatedList<WorkAttendanceDTO>>> GetAllPage(int pageSize, int pageNum, string? nameSearch, DateTimeOffset? dateTime);
	}
}

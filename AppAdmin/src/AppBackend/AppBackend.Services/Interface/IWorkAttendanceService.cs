using AppBackend.Application.Common.Response;
using AppBackend.Application.ModelsDTO;

namespace AppBackend.Application.Interface
{
	public interface IWorkAttendanceService
	{
		Task<Response<string>> Post();
		Task<Response<string>> Put(WorkAttendanceDTO workAttendanceDTO);
		Task<Response<string>> Remove();
		Task<Response<WorkAttendanceDTO>> Get(string id);
		Task<Response<string>> ComfirmByEmployee(string id);
		Task SendMailToEmployee();
		Task<Response<PaginatedList<WorkAttendanceDTO>>> GetAllPage(int pageSize, int pageNum, string? nameSearch, DateTimeOffset? dateTime);
		Task<Response<PaginatedList<WorkAttendanceDTO>>> GetAllByEmployee(int pageSize, int pageNum, DateTimeOffset? dateTime);
	}
}

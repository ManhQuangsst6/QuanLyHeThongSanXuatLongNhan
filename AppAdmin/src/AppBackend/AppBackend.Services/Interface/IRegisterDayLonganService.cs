using AppBackend.Application.Common.Response;
using AppBackend.Application.ModelsDTO;

namespace AppBackend.Application.Interface
{
	public interface IRegisterDayLonganService
	{
		Task<Response<string>> Post(RegisterDayLonganDTO registerDayLonganDTO);
		Task<Response<string>> Put(RegisterDayLonganDTO registerDayLonganDTO);
		Task<Response<string>> Remove(string id);
		Task<Response<RegisterDayLonganDTO>> Get(string id);
		Task<Response<PaginatedList<RegisterDayLonganDTO>>> GetAllPage(int pageSize, int pageNum, string? searchName, DateTimeOffset? dateTime);
	}
}

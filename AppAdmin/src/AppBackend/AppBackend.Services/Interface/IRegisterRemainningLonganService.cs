using AppBackend.Application.Common.Response;
using AppBackend.Application.ModelsDTO;

namespace AppBackend.Application.Interface
{
	public interface IRegisterRemainningLonganService
	{
		Task<Response<string>> Post(RegisterRemainningLonganDTO registerRemainningLonganDTO);
		Task<Response<string>> Put(RegisterRemainningLonganDTO registerRemainningLonganDTO);
		Task<Response<string>> Remove(string id);
		Task<Response<RegisterRemainningLonganDTO>> Get(string id);
		Task<Response<PaginatedList<RegisterRemainningLonganDTO>>> GetAllPage(int pageSize, int pageNum, string? searchName, DateTimeOffset? dateTime);
		Task<Response<PaginatedList<RegisterRemainningLonganDTO>>> GetAllPageUser(int pageSize, int pageNum, int? status, DateTimeOffset? dateTime);
	}
}

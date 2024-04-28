using AppBackend.Application.Common.Response;
using AppBackend.Application.ModelsDTO;

namespace AppBackend.Application.Interface
{
	public interface IEventService
	{
		Task<Response<string>> Post(EventDTO eventDTO);
		Task<Response<string>> Put(EventDTO eventDTO);
		Task<Response<string>> Remove(string id);
		Task<Response<EventDTO>> Get(string id);
		Task<Response<PaginatedList<EventDTO>>> GetAllPage(int pageSize, int pageNum, string? searchName);
	}
}

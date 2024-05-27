using AppBackend.Application.Common.Response;
using AppBackend.Application.ModelsDTO;

namespace AppBackend.Application.Interface
{
	public interface INotificationService
	{
		Task<Response<string>> Post(NotificationDTO eventDTO);
		Task<Response<int>> GetCount();
		Task<Response<string>> Read(string id);

		Task<Response<string>> Remove(string id);
		Task<Response<PaginatedList<NotificationDTO>>> GetAllPage(int pageSize, int pageNum);
	}
}

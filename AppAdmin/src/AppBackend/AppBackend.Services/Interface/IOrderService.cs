





using AppBackend.Application.Common.Response;
using AppBackend.Application.ModelsDTO;

namespace AppBackend.Application.Interface
{
	public interface IOrderService
	{
		Task<Response<string>> Post(OrderDTO orderDTO);
		Task<Response<string>> Put(OrderDTO orderDTO);
		Task<Response<string>> Remove(string id);
		Task<Response<OrderDTO>> Get(string id);
		Task<Response<PaginatedList<OrderDTO>>> GetAllPage(int pageSize, int pageNum, string? searchName,
			DateTimeOffset? dateFrom, DateTimeOffset? dateTo);
	}
}

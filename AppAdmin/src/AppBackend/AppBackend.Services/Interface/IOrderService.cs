





using AppBackend.Application.Common.Response;
using AppBackend.Application.ModelsDTO;
using AppBackend.Application.ModelsDTO.ViewModel;

namespace AppBackend.Application.Interface
{
	interface IOrderService
	{
		Task<Response<string>> Post(OrderViewModel orderViewModel);
		Task<Response<string>> Put(OrderViewModel orderViewModel);
		Task<Response<string>> Remove(string id);
		Task<Response<OrderDTO>> Get(string id);
		Task<Response<PaginatedList<OrderDTO>>> GetAllPage(int pageSize, int pageNum, string? searchName);
	}
}

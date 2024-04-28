using AppBackend.Application.Common.Response;
using AppBackend.Application.ModelsDTO;

namespace AppBackend.Application.Interface
{
	public interface IPurchaseOrderService
	{
		Task<Response<string>> Post(PurchaseOrderDTO IngredientDTO);
		Task<Response<string>> Put(PurchaseOrderDTO IngredientDTO);
		Task<Response<string>> Remove(string id);
		Task<Response<PurchaseOrderDTO>> Get(string id);
		Task<Response<PaginatedList<PurchaseOrderDTO>>> GetAllPage(int pageSize, int pageNum, string? searchName);
	}
}

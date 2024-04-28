using AppBackend.Application.Common.Response;
using AppBackend.Application.ModelsDTO;

namespace AppBackend.Application.Interface
{
	public interface IShipmentService
	{
		Task<Response<string>> Post(ShipmentDTO IngredientDTO);
		Task<Response<string>> Put(ShipmentDTO IngredientDTO);
		Task<Response<string>> Remove(string id);
		Task<Response<ShipmentDTO>> Get(string id);
		Task<Response<PaginatedList<ShipmentDTO>>> GetAllPage(int pageSize, int pageNum, string shipmentCode);
		Task<Response<List<ShipmentDTO>>> GetAll();
	}
}

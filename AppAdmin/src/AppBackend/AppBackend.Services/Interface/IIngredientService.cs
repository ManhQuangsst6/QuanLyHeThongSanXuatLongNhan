using AppBackend.Application.Common.Response;
using AppBackend.Application.ModelsDTO;

namespace AppBackend.Application.Interface
{
	public interface IIngredientService
	{
		Task<Response<string>> Post(IngredientDTO IngredientDTO);
		Task<Response<string>> Put(IngredientDTO IngredientDTO);
		Task<Response<string>> Remove(string id);
		Task<Response<IngredientDTO>> Get(string id);
		Task<Response<PaginatedList<IngredientDTO>>> GetAllPage(int pageSize, int pageNum);
		Task<Response<List<IngredientDTO>>> GetAll();
	}
}

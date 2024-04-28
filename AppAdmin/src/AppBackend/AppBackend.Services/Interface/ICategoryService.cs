using AppBackend.Application.Common.Response;
using AppBackend.Application.ModelsDTO;

namespace AppBackend.Application.Interface
{
	public interface ICategoryService
	{
		Task<Response<string>> Post(CategoryDTO categoryDTO);
		Task<Response<string>> Put(CategoryDTO categoryDTO);
		Task<Response<string>> Remove(string id);
		Task<Response<CategoryDTO>> Get(string id);
		Task<Response<List<CategoryDTO>>> GetAll();
	}
}

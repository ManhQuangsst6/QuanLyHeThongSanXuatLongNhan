using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppBackend.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize(Roles = "Employee,Manager")]
	public class IngredientController : ControllerBase
	{
		private readonly IIngredientService _ingredientService;
		public IngredientController(IIngredientService ingredientService)
		{
			_ingredientService = ingredientService;
		}

		[HttpGet]
		public async Task<IActionResult> GetByID(string id)
		{
			try
			{
				var result = await _ingredientService.Get(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpGet]
		public async Task<IActionResult> GetListAll()
		{
			try
			{
				var result = await _ingredientService.GetAll();
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpGet]
		public async Task<IActionResult> GetListByPage(int pageNum, int pageSize)
		{
			try
			{
				var result = await _ingredientService.GetAllPage(pageSize, pageNum);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpPost]
		public async Task<IActionResult> Post(IngredientDTO ingredientDTO)
		{
			try
			{
				var result = await _ingredientService.Post(ingredientDTO);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpPut]
		public async Task<IActionResult> Update(IngredientDTO ingredientDTO)
		{
			try
			{
				var result = await _ingredientService.Put(ingredientDTO);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpDelete]
		public async Task<IActionResult> Remove(string id)
		{
			try
			{
				var result = await _ingredientService.Remove(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
	}
}

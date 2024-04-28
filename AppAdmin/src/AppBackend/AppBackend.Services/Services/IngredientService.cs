using AppBackend.Application.Common.Mappings;
using AppBackend.Application.Common.Response;
using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using AppBackend.Data.Context;
using AppBackend.Data.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AppBackend.Application.Services
{
	public class IngredientService : IIngredientService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		public IngredientService(DataContext _dataContext, IMapper _mapper)
		{
			this._dataContext = _dataContext;
			this._mapper = _mapper;
		}
		public async Task<Response<IngredientDTO>> Get(string id)
		{
			var result = await _dataContext.Ingredients.FirstOrDefaultAsync(x => x.Id == id);
			if (result == null) return new Response<IngredientDTO>() { IsSuccess = false, Status = 404, Message = "Not Found Ingredient!" };
			return new Response<IngredientDTO>() { IsSuccess = true, Status = 200, Value = _mapper.Map<IngredientDTO>(result) };
		}

		public async Task<Response<List<IngredientDTO>>> GetAll()
		{
			try
			{
				var ListData = await _dataContext.Ingredients.ToListAsync();
				List<IngredientDTO> result = new List<IngredientDTO>();
				foreach (var item in ListData)
				{
					result.Add(_mapper.Map<IngredientDTO>(item));
				}
				return new Response<List<IngredientDTO>> { IsSuccess = true, Status = 200, Value = result };
			}
			catch (Exception ex)
			{
				return new Response<List<IngredientDTO>> { IsSuccess = false, Status = 404, Message = ex.ToString() };
			}
		}

		public async Task<Response<PaginatedList<IngredientDTO>>> GetAllPage(int pageSize, int pageNum)
		{
			var query = _dataContext.Ingredients.AsNoTracking();
			var objs = await query.ProjectTo<IngredientDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<IngredientDTO>> { IsSuccess = true, Status = 200, Value = objs };
		}

		public async Task<Response<string>> Post(IngredientDTO IngredientDTO)
		{
			try
			{
				IngredientDTO.Id = Guid.NewGuid().ToString();
				var Ingredient = _mapper.Map<Ingredient>(IngredientDTO);
				await _dataContext.Ingredients.AddAsync(Ingredient);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = IngredientDTO.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Put(IngredientDTO ingredientDTO)
		{
			try
			{
				var ingredient = await _dataContext.Ingredients.FirstOrDefaultAsync(x => x.Id == ingredientDTO.Id);
				if (ingredient == null) return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found Ingredient!" };
				ingredient.Name = ingredientDTO.Name;
				ingredient.Measure = ingredientDTO.Measure;
				ingredient.Description = ingredientDTO.Description;
				_dataContext.Ingredients.Update(ingredient);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = ingredientDTO.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Remove(string id)
		{
			try
			{
				var ingredient = await _dataContext.Ingredients.FirstOrDefaultAsync(x => x.Id == id);
				if (ingredient == null)
					return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found Ingredients " };
				_dataContext.Ingredients.Remove(ingredient);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}
	}
}

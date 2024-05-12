using AppBackend.Application.Common.Response;
using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using AppBackend.Data.Context;
using AppBackend.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppBackend.Application.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		public CategoryService(DataContext _dataContext, IMapper _mapper)
		{
			this._dataContext = _dataContext;
			this._mapper = _mapper;
		}
		public async Task<Response<CategoryDTO>> Get(string id)
		{
			var result = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
			if (result == null) return new Response<CategoryDTO>() { IsSuccess = false, Status = 404, Message = "Not Found Categories!" };
			return new Response<CategoryDTO>() { IsSuccess = true, Status = 200, Value = _mapper.Map<CategoryDTO>(result) };
		}

		public async Task<Response<List<CategoryDTO>>> GetAll()
		{
			try
			{
				var ListData = await _dataContext.Categories.ToListAsync();
				List<CategoryDTO> result = new List<CategoryDTO>();
				foreach (var item in ListData)
				{
					result.Add(_mapper.Map<CategoryDTO>(item));
				}
				return new Response<List<CategoryDTO>> { IsSuccess = true, Status = 200, Value = result };
			}
			catch (Exception ex)
			{
				return new Response<List<CategoryDTO>> { IsSuccess = false, Status = 404, Message = ex.ToString() };
			}
		}


		public async Task<Response<string>> Post(CategoryDTO categoryDTO)
		{
			try
			{
				categoryDTO.Id = Guid.NewGuid().ToString();
				var category = _mapper.Map<Category>(categoryDTO);
				await _dataContext.Categories.AddAsync(category);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = categoryDTO.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Put(CategoryDTO categoryDTO)
		{
			try
			{
				var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryDTO.Id);
				if (category == null) return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found Ingredient!" };
				category.Name = categoryDTO.Name;
				category.WholesalePrice = categoryDTO.WholesalePrice;
				category.RetailPrice = categoryDTO.RetailPrice;
				category.Description = categoryDTO.Description;
				_dataContext.Categories.Update(category);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = categoryDTO.Id };
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
				var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
				if (category == null)
					return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found Categories " };
				_dataContext.Categories.Remove(category);
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

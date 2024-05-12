using AppBackend.Application.Common.Mappings;
using AppBackend.Application.Common.Response;
using AppBackend.Application.ModelsDTO;
using AppBackend.Data.Context;
using AppBackend.Data.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AppBackend.Application.Services
{
	public class ComfirmLonganService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		public ComfirmLonganService(DataContext _dataContext, IMapper _mapper)
		{
			this._dataContext = _dataContext;
			this._mapper = _mapper;
		}
		public Task<Response<string>> Comfirm(string id)
		{
			throw new NotImplementedException();
		}

		public async Task<Response<ComfirmLonganDTO>> Get(string id)
		{
			var result = await _dataContext.ComfirmLongans.FirstOrDefaultAsync(x => x.Id == id);
			if (result == null) return new Response<ComfirmLonganDTO>() { IsSuccess = false, Status = 404, Message = "Not Found Categories!" };
			return new Response<ComfirmLonganDTO>() { IsSuccess = true, Status = 200, Value = _mapper.Map<ComfirmLonganDTO>(result) };
		}

		public async Task<Response<PaginatedList<ComfirmLonganDTO>>> GetAll(int pageSize, int pageNum, DateTimeOffset? dateCurrennt)
		{
			var query = _dataContext.ComfirmLongans.AsNoTracking().Where(x => !dateCurrennt.HasValue || (x.Created.Value.Year == dateCurrennt.Value.Year &&
			x.Created.Value.Month == dateCurrennt.Value.Month && x.Created.Value.Day == dateCurrennt.Value.Day));
			var objs = await query.ProjectTo<ComfirmLonganDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<ComfirmLonganDTO>> { IsSuccess = true, Status = 200, Value = objs };
		}

		public async Task<Response<string>> Post(ComfirmLonganDTO comfirmLonganDTO)
		{
			try
			{
				comfirmLonganDTO.Id = Guid.NewGuid().ToString();
				var category = _mapper.Map<ComfirmLongan>(comfirmLonganDTO);
				await _dataContext.ComfirmLongans.AddAsync(category);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = comfirmLonganDTO.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Put(ComfirmLonganDTO comfirmLonganDTO)
		{
			try
			{
				var comfirmLongan = await _dataContext.ComfirmLongans.FirstOrDefaultAsync(x => x.Id == comfirmLonganDTO.Id);
				if (comfirmLongan == null) return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found comfirmLongan!" };
				comfirmLongan.Amount = comfirmLonganDTO.Amount;
				_dataContext.Update(comfirmLongan);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = comfirmLongan.Id };
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
				var comfirmLongan = await _dataContext.ComfirmLongans.FirstOrDefaultAsync(x => x.Id == id);
				if (comfirmLongan == null)
					return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found ComfirmLongans " };
				_dataContext.ComfirmLongans.Remove(comfirmLongan);
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

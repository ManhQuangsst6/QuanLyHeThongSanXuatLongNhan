using AppBackend.Application.Common.Mappings;
using AppBackend.Application.Common.Response;
using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using AppBackend.Data.Context;
using AppBackend.Data.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using static AppBackend.Data.Enums.EnumData;

namespace AppBackend.Application.Services
{
	public class OrderService : IOrderService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		public OrderService(DataContext _dataContext, IMapper _mapper)
		{
			this._dataContext = _dataContext;
			this._mapper = _mapper;
		}
		public async Task<Response<OrderDTO>> Get(string id)
		{
			var result = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
			if (result == null) return new Response<OrderDTO>() { IsSuccess = false, Status = 404, Message = "Not Found Categories!" };
			return new Response<OrderDTO>() { IsSuccess = true, Status = 200, Value = _mapper.Map<OrderDTO>(result) };
		}

		public async Task<Response<PaginatedList<OrderDTO>>> GetAllPage(int pageSize, int pageNum, string? searchName, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
		{

			var query = _dataContext.Orders.AsNoTracking().Where(x => (dateFrom == null || x.Created >= dateFrom) &&
			(dateTo == null || x.Created <= dateTo) && (dateFrom == null && dateTo == null && x.Created.Value.Date == DateTimeOffset.Now.Date));
			var objs = await query.ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<OrderDTO>> { IsSuccess = true, Status = 200, Value = objs };
		}

		public async Task<Response<string>> Post(OrderDTO orderDTO)
		{
			try
			{
				orderDTO.Id = Guid.NewGuid().ToString();
				var order = _mapper.Map<Order>(orderDTO);
				await _dataContext.Orders.AddAsync(order);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = orderDTO.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public Task<Response<string>> Put(OrderDTO orderDTO)
		{
			throw new NotImplementedException();
		}

		public async Task<Response<string>> Remove(string id)
		{
			var order = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
			if (order == null)
				return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found Orders " };
			order.IsDeleted = (int)StatusIsDelete.Done;
			_dataContext.Orders.Update(order);
			await _dataContext.SaveChangesAsync();
			return new Response<string> { IsSuccess = true, Status = 200, Value = id };
		}
	}
}

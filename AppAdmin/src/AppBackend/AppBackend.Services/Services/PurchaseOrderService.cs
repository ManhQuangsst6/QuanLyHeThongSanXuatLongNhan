using AppBackend.Application.Common.Mappings;
using AppBackend.Application.Common.Response;
using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using AppBackend.Data.Context;
using AppBackend.Data.Enums;
using AppBackend.Data.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AppBackend.Application.Services
{
	public class PurchaseOrderService : IPurchaseOrderService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		public PurchaseOrderService(DataContext dataContext, IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}
		public async Task<Response<PurchaseOrderDTO>> Get(string id)
		{
			var result = await _dataContext.PurchaseOrders.FirstOrDefaultAsync(o => o.Id == id);
			return new Response<PurchaseOrderDTO>() { IsSuccess = true, Status = 200, Value = _mapper.Map<PurchaseOrderDTO>(result) };
		}

		public async Task<Response<PaginatedList<PurchaseOrderDTO>>> GetAllPage(int pageSize, int pageNum, string? searchName)
		{
			var purchaseOrders = _dataContext.PurchaseOrders.AsNoTracking();
			var employee = _dataContext.Employees.AsNoTracking();
			var ingredient = _dataContext.Ingredients.AsNoTracking();
			var query = from p in purchaseOrders
						join e in employee on p.EmployeeID equals e.Id
						join i in ingredient on p.IngredientID equals i.Id
						where (p.IngredientID == searchName || searchName.IsNullOrEmpty()) && p.IsDelete == (int)EnumData.StatusIsDelete.Doing
						orderby p.OrderDate
						select p;

			var objs = await query.ProjectTo<PurchaseOrderDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<PurchaseOrderDTO>> { IsSuccess = true, Status = 200, Value = objs };
		}

		public async Task<Response<string>> Post(PurchaseOrderDTO purchaseOrderDTO)
		{
			try
			{
				purchaseOrderDTO.Id = Guid.NewGuid().ToString();
				var purchaseOrder = _mapper.Map<PurchaseOrder>(purchaseOrderDTO);
				purchaseOrder.OrderDate = purchaseOrder.OrderDate.AddDays(1);
				purchaseOrder.IsDelete = (int)EnumData.StatusIsDelete.Doing;
				await _dataContext.PurchaseOrders.AddAsync(purchaseOrder);
				await _dataContext.SaveChangesAsync();

				return new Response<string> { IsSuccess = true, Status = 200, Value = purchaseOrder.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Put(PurchaseOrderDTO purchaseOrderDTO)
		{
			try
			{
				var purchaseOrder = await _dataContext.PurchaseOrders.FirstOrDefaultAsync(x => x.Id == purchaseOrderDTO.Id);
				if (purchaseOrder == null) return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found purchaseOrder!" };
				purchaseOrder.EmployeeID = purchaseOrderDTO.EmployeeID;
				purchaseOrder.OrderDate = purchaseOrderDTO.OrderDate.AddDays(1);
				purchaseOrder.Amount = purchaseOrderDTO.Amount;
				purchaseOrder.Note = purchaseOrderDTO.Note;
				purchaseOrder.Price = ((decimal)purchaseOrderDTO.Price);
				purchaseOrder.IngredientID = purchaseOrderDTO.IngredientID;
				_dataContext.PurchaseOrders.Update(purchaseOrder);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = purchaseOrder.Id };
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
				var purchaseOrders = await _dataContext.PurchaseOrders.FirstOrDefaultAsync(x => x.Id == id);
				if (purchaseOrders == null)
					return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found purchaseOrders " };
				purchaseOrders.IsDelete = (int)EnumData.StatusIsDelete.Done;
				_dataContext.PurchaseOrders.Update(purchaseOrders);
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

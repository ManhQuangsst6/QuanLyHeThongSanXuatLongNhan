using AppBackend.Application.Common.Mappings;
using AppBackend.Application.Common.Response;
using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using AppBackend.Data.Context;
using AppBackend.Data.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static AppBackend.Data.Enums.EnumData;

namespace AppBackend.Application.Services
{
	public class ShipmentService : IShipmentService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		public ShipmentService(DataContext _dataContext, IMapper _mapper)
		{
			this._dataContext = _dataContext;
			this._mapper = _mapper;
		}
		public async Task<Response<ShipmentDTO>> Get(string id)
		{
			var result = await _dataContext.Shipments.FirstOrDefaultAsync(x => x.Id == id);
			if (result == null) return new Response<ShipmentDTO>() { IsSuccess = false, Status = 404, Message = "Not Found Shipment!" };
			return new Response<ShipmentDTO>() { IsSuccess = true, Status = 200, Value = _mapper.Map<ShipmentDTO>(result) };
		}

		public Task<Response<List<ShipmentDTO>>> GetAll()
		{
			throw new NotImplementedException();
		}

		public async Task<Response<PaginatedList<ShipmentDTO>>> GetAllPage(int pageSize, int pageNum, string shipmentCode)
		{
			var employees = _dataContext.Employees.AsNoTracking();
			var shipments = _dataContext.Shipments.AsNoTracking();
			var categories = _dataContext.No.AsNoTracking();
			var query = from e in employees
						join s in shipments on e.Id equals s.EmployeeID
						join c in categories on s.CategoryID equals c.Id
						where (s.ShipmentCode.Contains(shipmentCode) || shipmentCode.IsNullOrEmpty()) && s.IsDelete == 0
						orderby s.DateUp
						select s;

			var result = await query.ProjectTo<ShipmentDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<ShipmentDTO>> { IsSuccess = true, Status = 200, Value = result };
		}

		public async Task<Response<string>> Post(ShipmentDTO shipmentDTO)
		{
			try
			{
				shipmentDTO.Id = Guid.NewGuid().ToString();
				var shipment = _mapper.Map<Shipment>(shipmentDTO);

				shipment.IsDelete = (int)StatusIsDelete.Doing;
				await _dataContext.Shipments.AddAsync(shipment);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = shipmentDTO.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Put(ShipmentDTO shipmentDTO)
		{
			try
			{
				var shipment = await _dataContext.Shipments.FirstOrDefaultAsync(x => x.Id == shipmentDTO.Id);
				if (shipment == null) return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found Shipment!" };
				shipment.DateFrom = shipmentDTO.DateFrom;
				shipment.DateTo = shipmentDTO.DateTo;
				shipment.Amount = shipmentDTO.Amount;
				shipment.CategoryID = shipment.CategoryID;
				shipment.EmployeeID = shipment.EmployeeID;
				_dataContext.Shipments.Update(shipment);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = shipment.Id };
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
				var shipment = await _dataContext.Shipments.FirstOrDefaultAsync(x => x.Id == id);
				if (shipment == null)
					return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found Shipments " };
				shipment.IsDelete = (int)StatusIsDelete.Done;
				_dataContext.Shipments.Update(shipment);
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

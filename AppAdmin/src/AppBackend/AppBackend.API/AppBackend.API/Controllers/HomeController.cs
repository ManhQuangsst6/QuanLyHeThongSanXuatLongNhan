using AppBackend.Data.Context;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppBackend.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize(Roles = "Manager")]
	public class HomeController : ControllerBase
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		public HomeController(DataContext _dataContext, IMapper _mapper)
		{
			this._dataContext = _dataContext;
			this._mapper = _mapper;
		}
		[HttpGet]
		public async Task<IActionResult> GetCountEmployye()
		{
			var num = await _dataContext.Employees.Where(x => x.IsDeleted == 0).CountAsync();
			return Ok(num);
		}
		[HttpGet]
		public async Task<IActionResult> GetCountShipment()
		{
			var num = await _dataContext.Shipments.Where(x => x.Status == 0 && x.IsDelete == 0).CountAsync();
			return Ok(num);
		}
		[HttpGet]
		public async Task<IActionResult> GetCountLogan()
		{
			var num = await _dataContext.Shipments.Where(x => x.Status == 0 && x.IsDelete == 0).SumAsync(x => x.Amount);
			return Ok(num);
		}
		[HttpGet]
		public async Task<IActionResult> GetLoganByCategory()
		{
			var res = await (from shipment in _dataContext.Shipments
							 join category in _dataContext.No
							 on shipment.CategoryID equals category.Id
							 // where shipment.IsDelete == 0 // Assuming you want to exclude deleted records
							 group new { shipment, category } by new { shipment.CategoryID, category.Name } into g
							 select new
							 {
								 CategoryID = g.Key.CategoryID,
								 CategoryName = g.Key.Name,
								 TotalAmount = g.Sum(x => x.shipment.Amount),
							 })
						.OrderBy(result => result.CategoryID)
						.ToListAsync();
			return Ok(res);
		}
		[HttpGet]
		public async Task<IActionResult> GetLonganCommon()
		{
			var res = await (from shipment in _dataContext.Shipments
							 join category in _dataContext.No
							 on shipment.CategoryID equals category.Id
							 // where shipment.IsDelete == 0 // Assuming you want to exclude deleted records
							 group new { shipment, category } by new { Year = shipment.DateFrom.Value.Year, shipment.CategoryID, category.Name } into g
							 select new
							 {
								 Year = g.Key.Year,
								 CategoryID = g.Key.CategoryID,
								 CategoryName = g.Key.Name,
								 TotalAmount = g.Sum(x => x.shipment.Amount),
							 })
						.OrderBy(result => result.Year)
						.ThenBy(result => result.CategoryID)
						.ToListAsync();
			var result = res
			.GroupBy(x => x.CategoryName)
			.Select(g => new
			{
				CategoryName = g.Key,
				listYear = g.Select(x => x.Year).ToList(),
				listTotalAmount = g.Select(x => x.TotalAmount).ToList()
			})
			.ToList();
			return Ok(result);
		}
	}
}

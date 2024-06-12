using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppBackend.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class SalaryController : ControllerBase
	{
		private readonly ISalaryService _salaryService;
		public SalaryController(ISalaryService salaryService)
		{
			_salaryService = salaryService;
		}
		[HttpGet]
		public async Task<IActionResult> GetByID(string id)
		{
			try
			{
				var result = await _salaryService.Get(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}

		[HttpGet]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> GetListByEmployeePage(int pageNum, int pageSize, string? searchName)
		{
			try
			{
				var result = await _salaryService.GetAllByEmployee(pageSize, pageNum, searchName);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		[HttpGet, Authorize]
		public async Task<IActionResult> GetAllClient(int pageNum, int pageSize, int? quarterYear, int? year)
		{
			try
			{
				var result = await _salaryService.GetAllClient(pageSize, pageNum, quarterYear, year);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		[HttpGet]
		[Authorize(Roles = "Employee,Manager")]
		public async Task<IActionResult> GetListByPage(int pageNum, int pageSize, string? searchName, int? quarterYear, int? year)
		{
			try
			{
				var result = await _salaryService.GetAllPage(pageSize, pageNum, searchName, quarterYear, year);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		[HttpGet]
		[Authorize(Roles = "Employee,Manager")]

		public async Task<IActionResult> GetAllExportExcel(int? quarterYear, int? year)
		{
			try
			{
				var result = await _salaryService.GetAllExportExcel(quarterYear, year);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		[HttpGet]
		[Authorize(Roles = "Employee,Manager")]
		public async Task<IActionResult> GetTableSalary(int pageNum = 1, int pageSize = 10, int? quarterYear = 1, int? year = 1)
		{
			try
			{
				var result = await _salaryService.GetTableSalary(pageSize, pageNum, quarterYear, year);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		[HttpGet]
		[Authorize(Roles = "Employee,Manager")]
		public async Task<IActionResult> CreateTableSalary(int price, int quarterYear, int year)
		{
			try
			{
				await _salaryService.CreateTableSalary(quarterYear, year, price);
				return Ok(year);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		[HttpPost]
		public async Task<IActionResult> Post(SalaryDTO salaryDTO)
		{
			try
			{
				var result = await _salaryService.Post(salaryDTO);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpPut, Authorize]
		public async Task<IActionResult> Update(SalaryDTO salaryDTO)
		{
			try
			{
				var result = await _salaryService.Put(salaryDTO);
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
				var result = await _salaryService.Remove(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}

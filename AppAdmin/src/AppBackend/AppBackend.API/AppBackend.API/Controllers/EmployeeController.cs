using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using Microsoft.AspNetCore.Mvc;

namespace AppBackend.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeService employeeService;
		public EmployeeController(IEmployeeService employeeService)
		{
			this.employeeService = employeeService;
		}
		[HttpGet]
		public async Task<IActionResult> GetByID(string id)
		{
			try
			{
				var result = await employeeService.Get(id);
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
				var result = await employeeService.GetAllPage(pageSize, pageNum);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpGet]
		public async Task<IActionResult> GetListEmployeePage()
		{
			try
			{
				var result = await employeeService.GetAllEmployee();
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpGet]
		public async Task<IActionResult> GetAllUserPage(int pageNum, int pageSize, string? nameSearch)
		{
			try
			{
				var result = await employeeService.GetAllUserPage(nameSearch, pageSize, pageNum);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpPost]
		public async Task<IActionResult> Post(EmployeeDTO employeeDTO)
		{
			try
			{
				var result = await employeeService.Post(employeeDTO);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpPut]
		public async Task<IActionResult> Update(EmployeeDTO employeeDTO)
		{
			try
			{
				var result = await employeeService.Put(employeeDTO);
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
				var result = await employeeService.Remove(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}

	}
}

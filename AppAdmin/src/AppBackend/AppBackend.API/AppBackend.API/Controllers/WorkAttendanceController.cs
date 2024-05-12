using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using Microsoft.AspNetCore.Mvc;

namespace AppBackend.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class WorkAttendanceController : ControllerBase
	{
		private readonly IWorkAttendanceService _workAttendanceService;
		public WorkAttendanceController(IWorkAttendanceService workAttendanceService)
		{
			_workAttendanceService = workAttendanceService;
		}

		[HttpGet]
		public async Task<IActionResult> GetByID(string id)
		{
			try
			{
				var result = await _workAttendanceService.Get(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpGet]
		public async Task<IActionResult> GetListByPage(int pageNum, int pageSize, string? searchName, DateTimeOffset? dateTime)
		{
			try
			{
				var result = await _workAttendanceService.GetAllPage(pageSize, pageNum, searchName, dateTime);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		[HttpPost]
		public async Task<IActionResult> Post()
		{
			try
			{
				var result = await _workAttendanceService.Post();
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpPut]
		public async Task<IActionResult> Update(WorkAttendanceDTO workAttendanceDTO)
		{
			try
			{
				var result = await _workAttendanceService.Put(workAttendanceDTO);
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
				var result = await _workAttendanceService.Remove(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpGet]
		public async Task<IActionResult> ComfirmByEmployee(string id, string employeeID)
		{
			try
			{
				var result = await _workAttendanceService.ComfirmByEmployee(id, employeeID);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}

using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
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
		[Authorize(Roles = "Employee,Manager")]
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
		[Authorize(Roles = "Employee,Manager")]
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
		[Authorize(Roles = "Employee,Manager")]
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
		[Authorize(Roles = "Employee,Manager")]
		public async Task<IActionResult> Remove()
		{
			try
			{
				var result = await _workAttendanceService.Remove();
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		[HttpGet, Authorize]
		public async Task<IActionResult> ComfirmByEmployee(string id)
		{
			try
			{
				var result = await _workAttendanceService.ComfirmByEmployee(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		[HttpGet]
		public async Task<IActionResult> GetAllByEmployee(int pageNum, int pageSize, DateTimeOffset? dateTime)
		{
			try
			{
				var result = await _workAttendanceService.GetAllByEmployee(pageSize, pageNum, dateTime);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}

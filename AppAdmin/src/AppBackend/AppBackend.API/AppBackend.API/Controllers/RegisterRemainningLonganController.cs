using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppBackend.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class RegisterRemainningLonganController : ControllerBase
	{
		private readonly IRegisterRemainningLonganService _registerRemainningLonganService;
		public RegisterRemainningLonganController(IRegisterRemainningLonganService registerRemainningLonganService)
		{
			_registerRemainningLonganService = registerRemainningLonganService;
		}
		[HttpGet]
		public async Task<IActionResult> GetByID(string id)
		{
			try
			{
				var result = await _registerRemainningLonganService.Get(id);
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
				var result = await _registerRemainningLonganService.GetAllPage(pageSize, pageNum, searchName, dateTime);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpGet]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> GetAllPageByUser(int pageNum, int pageSize, int? status, DateTimeOffset? dateTime)
		{
			try
			{
				var result = await _registerRemainningLonganService.GetAllPageUser(pageSize, pageNum, status, dateTime);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpPost]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> Post(RegisterRemainningLonganDTO registerRemainningLonganDTO)
		{
			try
			{
				var result = await _registerRemainningLonganService.Post(registerRemainningLonganDTO);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpPut, Authorize]
		public async Task<IActionResult> Update(RegisterRemainningLonganDTO registerRemainningLonganDTO)
		{
			try
			{
				var result = await _registerRemainningLonganService.Put(registerRemainningLonganDTO);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpDelete, Authorize]
		public async Task<IActionResult> Remove(string id)
		{
			try
			{
				var result = await _registerRemainningLonganService.Remove(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
	}
}

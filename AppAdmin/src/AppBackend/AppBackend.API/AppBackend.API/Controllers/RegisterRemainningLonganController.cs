using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
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
		[HttpPost]
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
		[HttpPut]
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
		[HttpDelete]
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

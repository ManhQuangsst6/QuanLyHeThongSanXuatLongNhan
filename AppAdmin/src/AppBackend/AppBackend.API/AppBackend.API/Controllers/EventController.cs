using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppBackend.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize(Roles = "Employee,Manager")]
	public class EventController : ControllerBase
	{
		private readonly IEventService _eventService;
		public EventController(IEventService eventService)
		{
			_eventService = eventService;
		}
		[HttpGet]
		public async Task<IActionResult> GetByID(string id)
		{
			try
			{
				var result = await _eventService.Get(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}

		[HttpGet]
		public async Task<IActionResult> GetListByPage(int pageNum, int pageSize, string? searchName)
		{
			try
			{
				var result = await _eventService.GetAllPage(pageSize, pageNum, searchName);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpPost]
		public async Task<IActionResult> Post(EventDTO eventDTO)
		{
			try
			{
				var result = await _eventService.Post(eventDTO);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpPut]
		public async Task<IActionResult> Update(EventDTO eventDTO)
		{
			try
			{
				var result = await _eventService.Put(eventDTO);
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
				var result = await _eventService.Remove(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
	}
}

﻿using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppBackend.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class RegisterDayLonganController : ControllerBase
	{
		private readonly IRegisterDayLonganService _registerDayLonganService;
		public RegisterDayLonganController(IRegisterDayLonganService registerDayLonganService)
		{
			_registerDayLonganService = registerDayLonganService;
		}
		[HttpGet]
		public async Task<IActionResult> GetByID(string id)
		{
			try
			{
				var result = await _registerDayLonganService.Get(id);
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
				var result = await _registerDayLonganService.GetAllPage(pageSize, pageNum, searchName, dateTime);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpPost, Authorize]
		public async Task<IActionResult> Post(RegisterDayLonganDTO registerDayLonganDTO)
		{
			try
			{
				var result = await _registerDayLonganService.Post(registerDayLonganDTO);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpPut]
		public async Task<IActionResult> Update(RegisterDayLonganDTO registerDayLonganDTO)
		{
			try
			{
				var result = await _registerDayLonganService.Put(registerDayLonganDTO);
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
				var result = await _registerDayLonganService.Remove(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
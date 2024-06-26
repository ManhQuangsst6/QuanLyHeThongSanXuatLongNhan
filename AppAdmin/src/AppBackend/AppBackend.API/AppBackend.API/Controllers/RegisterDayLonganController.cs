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
		private readonly INotificationService _notificationService;

		public RegisterDayLonganController(IRegisterDayLonganService registerDayLonganService, INotificationService notificationService)
		{
			_registerDayLonganService = registerDayLonganService;
			_notificationService = notificationService;
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
		[Authorize(Roles = "Employee,Manager")]
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
		[HttpGet]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> GetListByEmployee(int pageNum, int pageSize, int? status, DateTimeOffset? dateTime)
		{
			try
			{
				var result = await _registerDayLonganService.GetAllPageByUser(pageSize, pageNum, status, dateTime);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpPost]
		[Authorize(Roles = "User")]
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
		[HttpPut, Authorize]
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
		[HttpDelete, Authorize]

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

﻿using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppBackend.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize(Roles = "Employee,Manager")]
	public class PurchaseOrdersController : ControllerBase
	{
		private readonly IPurchaseOrderService _purchaseOrderService;
		public PurchaseOrdersController(IPurchaseOrderService purchaseOrderService)
		{
			_purchaseOrderService = purchaseOrderService;
		}
		[HttpGet]
		public async Task<IActionResult> GetByID(string id)
		{
			try
			{
				var result = await _purchaseOrderService.Get(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}

		[HttpGet]
		public async Task<IActionResult> GetListByPage(int pageNum, int pageSize, string? searchName, DateTimeOffset? date)
		{
			try
			{
				var result = await _purchaseOrderService.GetAllPage(pageSize, pageNum, searchName, date);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpPost]
		public async Task<IActionResult> Post(PurchaseOrderDTO purchaseOrderDTO)
		{
			try
			{
				var result = await _purchaseOrderService.Post(purchaseOrderDTO);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		[HttpPut]
		public async Task<IActionResult> Update(PurchaseOrderDTO purchaseOrderDTO)
		{
			try
			{
				var result = await _purchaseOrderService.Put(purchaseOrderDTO);
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
				var result = await _purchaseOrderService.Remove(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
	}
}

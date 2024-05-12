using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using Microsoft.AspNetCore.Mvc;

namespace AppBackend.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;
		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}
		[HttpPost]
		public async Task<IActionResult> Post(OrderDTO orderDTO)
		{
			try
			{
				var result = await _orderService.Post(orderDTO);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
	}
}

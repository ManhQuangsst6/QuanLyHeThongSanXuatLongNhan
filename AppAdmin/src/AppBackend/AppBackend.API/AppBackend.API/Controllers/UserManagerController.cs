using AppBackend.Application.Common.Exceptions;
using AppBackend.Application.Common.Interface;
using AppBackend.Data.Models.LogIn;
using AppBackend.Data.Models.SignUp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppBackend.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserManagerController : ControllerBase
	{
		private readonly IUserManager userManager;
		public UserManagerController(IUserManager userManager)
		{
			this.userManager = userManager;
		}
		[HttpGet("AllRoles")]
		public async Task<IActionResult> GetAllRole()
		{
			var a = await userManager.GetAllRole();
			return Ok(a);
		}
		[HttpPost]
		[Authorize(Roles = "Employee,Manager")]
		public async Task<IActionResult> RegisterUser([FromBody] RegisterUser registerUser)
		{
			try
			{
				var result = await userManager.RegisterUser(registerUser);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new NotFoundException(ex.Message);
			}
		}
		[HttpGet("ConfirmEmail")]
		public async Task<IActionResult> ConfirmEmail(string token, string email)
		{
			try
			{
				var result = await userManager.ConfirmEmail(token, email);
				return Ok($"<h1>{result.Message}</h1>");
			}
			catch (Exception ex)
			{
				throw new NotFoundException(ex.Message);
			}
		}
		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginModel userlogin)
		{
			try
			{
				var resutl = await userManager.Login(userlogin);
				return Ok(resutl);
			}
			catch (Exception ex)
			{
				throw new NotFoundException(ex.Message);
			}
		}

		[HttpPost("Login-2FA")]
		public async Task<IActionResult> LoginWithOTP(string code, string userName)
		{
			try
			{
				var resutl = await userManager.LoginWithOTP(code, userName);
				return Ok(resutl);
			}
			catch (Exception ex)
			{
				throw new NotFoundException(ex.Message);
			}
		}
	}
}

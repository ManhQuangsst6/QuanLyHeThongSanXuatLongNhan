using AppBackend.Application.Common.Interface;
using AppBackend.Application.Common.Response;
using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using AppBackend.Data.Context;
using AppBackend.Data.Models.Email;
using AppBackend.Data.Models.LogIn;
using AppBackend.Data.Models.SignUp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using static AppBackend.Data.Enums.EnumData;

namespace AppBackend.Application.Common.Services
{
	public class UserManager : IUserManager
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IConfiguration _configuration;
		private readonly IEmailService _emailService;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly IEmployeeService _employeeService;
		private readonly DataContext _dataContext;

		public UserManager(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
			IConfiguration configuration, IEmailService emailService, SignInManager<IdentityUser> signInManager,
			IHttpContextAccessor httpContextAccessor, IEmployeeService employeeService, DataContext dataContext)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_configuration = configuration;
			_emailService = emailService;
			_signInManager = signInManager;
			_httpContextAccessor = httpContextAccessor;
			_employeeService = employeeService;
			_dataContext = dataContext;
		}

		public async Task<Response<IdentityResult>> RegisterUser(RegisterUser registerUser)
		{
			try
			{
				if (registerUser == null)
				{
					throw new Exception("Error: RegisterUser is null");
				}
				var user = await _userManager.FindByNameAsync(registerUser.UserName);
				if (user != null)
				{
					throw new Exception("UserName is already exists already");
				}
				IdentityUser identityUser = new()
				{
					SecurityStamp = Guid.NewGuid().ToString(),
					UserName = registerUser.UserName,
					Email = registerUser.Email,
				};
				var roleCheck = await _roleManager.RoleExistsAsync(registerUser.Role);
				if (roleCheck)
				{
					registerUser.Password = "String1!";
					var result = await _userManager.CreateAsync(identityUser, registerUser.Password);
					var resultRole = await _userManager.AddToRoleAsync(identityUser, registerUser.Role);
					if (!registerUser.Email.IsNullOrEmpty())
					{
						var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
						string codeHtmlVersion = HttpUtility.UrlEncode(token);

						var a = _httpContextAccessor.HttpContext.Request;
						var request = _httpContextAccessor.HttpContext.Request;
						var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
						var confirmLink = baseUrl + $"/api/UserManager/ConfirmEmail?token={codeHtmlVersion}&email={registerUser.Email}";
						//var confirmLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = registerUser.Email }, Request.Scheme);
						Message message = new Message(new string[] { registerUser.Email! }, "Confirmation email link: ", confirmLink!);
						await _emailService.SendEmail(message);

					}
					var employeeCode = await GenerateEmployee(identityUser.Id, registerUser);
					return new Response<IdentityResult>() { Status = 200, IsSuccess = true, Message = employeeCode };
				}
				return new Response<IdentityResult>() { Status = 400, IsSuccess = false, Message = "Register user is fail" };
			}
			catch (Exception ex)
			{
				throw new Exception("Error: " + ex.Message);
			}
		}

		public async Task<Response<string>> ConfirmEmail(string token, string email)
		{
			try
			{
				var user = await _userManager.FindByEmailAsync(email);
				if (user == null) throw new Exception("Khong thay email");
				var result = await _userManager.ConfirmEmailAsync(user, token);
				if (result.Succeeded)
				{
					return new Response<string>() { Status = 200, IsSuccess = true, Message = "ConfirmEmail success!" };
				}
				return new Response<string>() { Status = 400, IsSuccess = false, Message = "ConfirmEmail fail!" };
			}
			catch (Exception ex)
			{
				throw new Exception("Error :" + ex.Message);
			}
		}

		public async Task<Response<UserLogin>> Login(LoginModel userlogin)
		{
			try
			{
				var user = await _userManager.FindByNameAsync(userlogin.UserName);
				await _signInManager.SignOutAsync();
				await _signInManager.PasswordSignInAsync(user, userlogin.Password, false, true);
				if (user != null && user.TwoFactorEnabled)
				{
					var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
					Message message = new Message(new string[] { user.Email! }, "Confirmation email OTP: ", token!);
					await _emailService.SendEmail(message);
					return new Response<UserLogin>() { Status = 200, IsSuccess = true, Message = "Send OTP to email success!" };
				}
				var authClaim = new List<Claim>() {
						new Claim(ClaimTypes.Name,userlogin.UserName),
						new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
					};
				var userRole = await _userManager.GetRolesAsync(user);
				foreach (var role in userRole)
				{
					authClaim.Add(new Claim(ClaimTypes.Role, role));
				}
				var jwtToken = GetToken(authClaim);
				var result = new UserLogin
				{
					Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
					Expiretion = jwtToken.ValidTo
				};
				return new Response<UserLogin>() { Status = 200, IsSuccess = true, Value = result };
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to login {ex.Message}");
			}
		}
		private JwtSecurityToken GetToken(List<Claim> claims)
		{
			SymmetricSecurityKey authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:ValidIssuer"],
				audience: _configuration["Jwt:ValidAudience"],
				expires: DateTime.Now.AddHours(1),
				claims: claims,
				signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
				);
			return token;
		}

		public async Task<Response<UserLogin>> LoginWithOTP(string code, string userName)
		{
			try
			{
				var user = await _userManager.FindByNameAsync(userName);
				await _signInManager.TwoFactorSignInAsync("Email", code, false, false);
				if (user != null)
				{
					var authClaim = new List<Claim>() {
						new Claim(ClaimTypes.Name,userName),
						new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
					};
					var userRole = await _userManager.GetRolesAsync(user);
					foreach (var role in userRole)
					{
						authClaim.Add(new Claim(ClaimTypes.Role, role));
					}

					var jwtToken = GetToken(authClaim);
					var result = new UserLogin
					{
						Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
						Expiretion = jwtToken.ValidTo
					};
					return new Response<UserLogin>() { Status = 200, IsSuccess = true, Value = result };
				}
				return new Response<UserLogin>() { Status = 404, IsSuccess = true, Message = "Fail!" };

			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to login {ex.Message}");
			}
		}

		private async Task<string> GenerateEmployee(string id, RegisterUser registerUser)
		{
			var employeeCode = GenericEmployeeCode(registerUser.FullName);
			EmployeeDTO employeeDTO = new EmployeeDTO()
			{
				Id = id,
				EmployeeCode = employeeCode,
				FullName = registerUser.FullName,
				ImageLink = "https://dakhoa.phongkhamdakhoahongphong.vn/upload/images/avatar/avatar-trong.png",
				//PositionID = registerUser.PositionID,
				Status = (int)StatusEmployee.Active
			};
			await _employeeService.Post(employeeDTO);
			return employeeCode;
		}

		public async Task<List<IdentityRole>> GetAllRole()
		{
			var roleStore = new RoleStore<IdentityRole>(_dataContext);
			var a = await roleStore.Roles.ToListAsync();

			return a;
		}
		private string GenericEmployeeCode(string name)
		{
			var arr = name.ToLower().Split(' ');
			var result = "";
			result += arr[arr.Count() - 1];
			for (int i = 0; i < arr.Count() - 1; i++)
			{
				result += arr[i][0];
			}

			var listName = _dataContext.Employees.Select(x => x.EmployeeCode).AsNoTracking().ToList();
			if (listName.Count > 0)
			{
				if (listName.Contains(result))
				{
					int i = 1;
					var sub = result;
					while (listName.Contains(sub))
					{
						sub = result + i.ToString();
						i++;
					}
					return sub;
				}
				else return result;
			}
			return result;

		}
	}
}


using AppBackend.Application.Common.Response;
using AppBackend.Data.Models.LogIn;
using AppBackend.Data.Models.SignUp;
using Microsoft.AspNetCore.Identity;

namespace AppBackend.Application.Common.Interface
{
	public interface IUserManager
	{
		Task<Response<IdentityResult>> RegisterUser(RegisterUser registerUser);
		Task<Response<string>> ConfirmEmail(string token, string email);
		Task<Response<UserLogin>> Login(LoginModel registerUser);
		Task<Response<UserLogin>> LoginWithOTP(string code, string userName);
		Task<List<IdentityRole>> GetAllRole();
	}
}

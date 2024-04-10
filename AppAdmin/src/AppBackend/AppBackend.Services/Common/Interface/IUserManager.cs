
using AppBackend.Data.Models.SignUp;
using Azure;
using Microsoft.AspNetCore.Identity;

namespace AppBackend.Application.Common.Interface
{
	public interface IUserManager
	{
		public Task<Response<IdentityResult>> RegisterUser(RegisterUser registerUser);
	}
}

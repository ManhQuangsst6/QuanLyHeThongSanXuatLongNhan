using AppBackend.Application.Common.Response;
using AppBackend.Data.Models.Email;

namespace AppBackend.Application.Interface
{
	public interface IEmailService
	{
		Task<Response<string>> SendEmail(Message message);
	}
}

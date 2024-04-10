using AppBackend.Application.Common.Response;
using MimeKit;

namespace AppBackend.Application.Interface
{
	public interface IEmailService
	{
		Task<Response<string>> SendEmail(MimeMessage message);
	}
}

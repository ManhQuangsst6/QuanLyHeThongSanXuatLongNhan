using AppBackend.Application.Common.Response;
using AppBackend.Application.Interface;
using AppBackend.Data.Models.Email;
using MailKit.Net.Smtp;
using MimeKit;


namespace AppBackend.Application.Services
{
	internal class EmailService : IEmailService
	{
		private readonly EmailConfiguration emailConfiguration;
		public EmailService(EmailConfiguration emailConfiguration)
		{
			this.emailConfiguration = emailConfiguration;
		}

		public async Task<Response<string>> SendEmail(MimeMessage message)
		{
			SmtpClient mailClient = new SmtpClient();
			try
			{
				await mailClient.ConnectAsync(emailConfiguration.SmtpServer, emailConfiguration.Port);
				mailClient.AuthenticationMechanisms.Remove("XOAUTH2");
				await mailClient.AuthenticateAsync(emailConfiguration.UserName, emailConfiguration.Password);
				await mailClient.SendAsync(message);
				return new Response<string> { Status = 200, Message = "Send email success!", IsSuccess = true };
			}
			catch (Exception ex)
			{
				return new Response<string> { Status = 404, Message = "Send email fail!", IsSuccess = false };
			}
		}
	}
}

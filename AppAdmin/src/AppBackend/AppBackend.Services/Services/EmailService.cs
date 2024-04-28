using AppBackend.Application.Common.Response;
using AppBackend.Application.Interface;
using AppBackend.Data.Models.Email;
using MailKit.Net.Smtp;
using MimeKit;


namespace AppBackend.Application.Services
{
	public class EmailService : IEmailService
	{
		private readonly EmailConfiguration emailConfiguration;
		public EmailService(EmailConfiguration emailConfiguration)
		{
			this.emailConfiguration = emailConfiguration;
		}
		public async Task<Response<string>> SendEmail(Message email)
		{
			try
			{
				var emailMessage = CreateMessage(email);
				await Send(emailMessage);
				return new Response<string> { Status = 200, Message = "Send email success!", IsSuccess = true };
			}
			catch (Exception ex)
			{
				return new Response<string> { Status = 404, Message = "Send email fail: " + ex.Message, IsSuccess = false };

			}
		}
		private MimeMessage CreateMessage(Message message)
		{
			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress("email", emailConfiguration.From));
			emailMessage.To.AddRange(message.To);
			emailMessage.Subject = message.Subject;
			emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
			return emailMessage;
		}
		public async Task<Response<string>> Send(MimeMessage message)
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
				throw new Exception("Error" + ex.Message);
			}
		}
	}
}

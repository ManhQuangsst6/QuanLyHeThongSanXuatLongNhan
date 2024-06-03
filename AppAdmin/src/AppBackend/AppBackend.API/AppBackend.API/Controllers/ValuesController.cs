using AppBackend.Application.Interface;
using AppBackend.Data.Models.Email;
using Microsoft.AspNetCore.Mvc;

namespace AppBackend.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly IEmailService _emailService;
		public ValuesController(IEmailService emailService)
		{
			_emailService = emailService;
		}
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var date = DateTime.Now.ToString("dd/MM/yyyy");

			var aaa = @"
<html lang=""en"">
    <head>    
        <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
        <title>
            Upcoming topics
        </title>
        <style type=""text/css"">
            HTML{background-color: #e8e8e8;}
            .courses-table{font-size: 12px; padding: 3px; border-collapse: collapse; border-spacing: 0;}
            .courses-table .description{color: #505050;}
            .courses-table td{border: 1px solid #D1D1D1; background-color: #F3F3F3; padding: 0 10px;}
            .courses-table th{border: 1px solid #424242; color: #FFFFFF;text-align: left; padding: 0 10px;}
            .green{background-color: #6B9852;}
        </style>
    </head>
    <body>
        <table class=""courses-table"">
            <thead>
                <tr>
                    <th class=""green"">Ngày</th>
                    <th class=""green"">Số cân</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class=""description"">" + date + @"</td>
                    <td>47</td>
                </tr>
               
            </tbody>
        </table>
    </body>
</html>
";
			Message message = new Message(new string[] { "daoquangsst6@gmail.com" }, "Thông báo số cân ngày " + date, aaa);
			await _emailService.SendEmail(message);
			return Ok("");
		}
	}
}

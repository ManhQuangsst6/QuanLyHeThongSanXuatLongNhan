namespace AppBackend.Data.Models.SignUp
{
	public class RegisterUser
	{
		public string FullName { get; set; }
		public string? UserName { get; set; } = String.Empty;
		public string? EmployeeCode { get; set; } = String.Empty;

		public string? Password { get; set; } = String.Empty;
		public string? Email { get; set; } = String.Empty;
		public string? Role { get; set; }
		//public string? PositionID { get; set; } = String.Empty;
	}
}

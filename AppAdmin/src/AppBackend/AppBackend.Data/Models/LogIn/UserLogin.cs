namespace AppBackend.Data.Models.LogIn
{
	public class UserLogin
	{
		public string Token { get; set; }
		public DateTime Expiretion { get; set; }
	}
}

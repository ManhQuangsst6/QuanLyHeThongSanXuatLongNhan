using AppBackend.Data.Common;

namespace AppBackend.Data.Models
{
	public class RegisterDayLongan : BaseAuditableEntity
	{
		public string Id { get; set; }
		public string EmployeeID { get; set; }
		public int Amount { get; set; }
		public int Ischeck { get; set; }

		public Employee Employee { get; set; }
	}
}

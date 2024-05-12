using AppBackend.Data.Common;

namespace AppBackend.Data.Models
{
	public class ComfirmLongan : BaseAuditableEntity
	{
		public string Id { get; set; }
		public string EmployeeID { get; set; }
		public double Amount { get; set; }
		public string? Note { get; set; }
		public int? IsComfirm { get; set; }
		public Employee Employee { get; set; }
	}
}

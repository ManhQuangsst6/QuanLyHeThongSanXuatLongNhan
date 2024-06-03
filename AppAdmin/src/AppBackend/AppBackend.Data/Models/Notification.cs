using AppBackend.Data.Common;

namespace AppBackend.Data.Models
{
	public class Notification : BaseAuditableEntity
	{
		public string Id { get; set; }
		public string? EmployeeReceive { get; set; }
		public string? EmployeeRole { get; set; }
		public string Content { get; set; }
		public string? Link { get; set; }
		public int? IsRead { get; set; }
	}
}

using AppBackend.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppBackend.Data.Models
{
	public class Event : BaseAuditableEntity
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string EmployeeID { get; set; }
		public DateTime DateStart { get; set; }
		[Column(TypeName = "decimal(18,3)")]
		public decimal Expense { get; set; }
		public Employee Employee { get; set; }
	}
}

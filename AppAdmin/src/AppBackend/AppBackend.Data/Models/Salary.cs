using AppBackend.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppBackend.Data.Models
{
	public class Salary : BaseAuditableEntity
	{
		public string Id { get; set; }
		public string EmployeeID { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }
		public DateTime DateUp { get; set; }
		[Column(TypeName = "decimal(18,3)")]
		public decimal SalaryMoney { get; set; }
		public Employee Employee { get; set; }
	}
}

using AppBackend.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppBackend.Data.Models
{
	public class Salary : BaseAuditableEntity
	{
		public string Id { get; set; }
		public string EmployeeID { get; set; }
		public string? Note { get; set; }
		public int? QuarterYear { get; set; }
		public int? Year { get; set; }
		[Column(TypeName = "decimal(18,3)")]
		public decimal SalaryMoney { get; set; }
		public int SumAmount { get; set; }
		public int Status { get; set; } = 0;
		public Employee Employee { get; set; }
	}
}

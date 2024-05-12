using AppBackend.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppBackend.Data.Models
{
	public class PurchaseOrder : BaseAuditableEntity
	{
		public string Id { get; set; }
		public string EmployeeID { get; set; }
		public DateTimeOffset OrderDate { get; set; }
		public int Amount { get; set; }
		public string? Note { get; set; }
		[Column(TypeName = "decimal(18,3)")]
		public decimal Price { get; set; }
		public string IngredientID { get; set; }
		public int? IsDelete { get; set; }
		public Ingredient Ingredient { get; set; }
		public Employee Employee { get; set; }
	}
}

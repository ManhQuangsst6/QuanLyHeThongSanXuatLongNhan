using AppBackend.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppBackend.Data.Models
{
	public class Category : BaseAuditableEntity
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; } = String.Empty;
		[Column(TypeName = "decimal(18,3)")]
		public decimal WholesalePrice { get; set; }
		[Column(TypeName = "decimal(18,3)")]
		public decimal RetailPrice { get; set; }
	}
}

using AppBackend.Data.Common;

namespace AppBackend.Data.Models
{
	public class Ingredient : BaseAuditableEntity
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string Measure { get; set; }
		public ICollection<PurchaseOrder>? PurchaseOrders { get; set; } = new List<PurchaseOrder>();
	}
}

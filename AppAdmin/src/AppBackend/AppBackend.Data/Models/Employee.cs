using AppBackend.Data.Common;

namespace AppBackend.Data.Models
{
	public class Employee : BaseAuditableEntity
	{
		public string Id { get; set; }
		public string EmployeeCode { get; set; }
		public string? FullName { get; set; }
		public string? ImageLink { get; set; }
		public int Status { get; set; }
		public int? IsDeleted { get; set; }
		public ICollection<Shipment>? Shipment { get; set; } = new List<Shipment>();
		public ICollection<PurchaseOrder>? PurchaseOrders { get; set; } = new List<PurchaseOrder>();
		public ICollection<Event>? Event { get; set; } = new List<Event>();
		public ICollection<Order>? Order { get; set; } = new List<Order>();
	}
}

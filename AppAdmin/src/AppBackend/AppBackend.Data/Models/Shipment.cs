using AppBackend.Data.Common;

namespace AppBackend.Data.Models
{
	public class Shipment : BaseAuditableEntity
	{
		public string Id { get; set; }
		public string ShipmentCode { get; set; }
		public DateTimeOffset? DateFrom { get; set; }
		public DateTimeOffset? DateTo { get; set; }
		public DateTimeOffset? DateUp { get; set; }
		public string EmployeeID { get; set; }
		public int Status { get; set; }
		public string CategoryID { get; set; }

		public double? Amount { get; set; }
		public double? Remainning { get; set; }
		public int? IsDelete { get; set; }
		public Employee Employee { get; set; }
		public Category Category { get; set; }
		public ICollection<Order_Shipment> Order_Shipments { get; set; } = new List<Order_Shipment>();
	}
}

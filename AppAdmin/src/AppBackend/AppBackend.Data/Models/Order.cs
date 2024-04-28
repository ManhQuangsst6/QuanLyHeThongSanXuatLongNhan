using AppBackend.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppBackend.Data.Models
{
	public class Order : BaseAuditableEntity
	{
		public string Id { get; set; }
		public string EmployeeID { get; set; }
		public DateTime OrderDate { get; set; }
		public string Note { get; set; }
		public int Amount { get; set; }
		[Column(TypeName = "decimal(18,3)")]
		public decimal Price { get; set; }
		public string CategoryLongan { get; set; }
		public string Customer { get; set; }
		public Employee Employee { get; set; }
		public ICollection<Order_Shipment> Order_Shipments { get; set; } = new List<Order_Shipment>();
	}
}

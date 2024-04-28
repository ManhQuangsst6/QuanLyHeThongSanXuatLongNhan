using System.ComponentModel.DataAnnotations.Schema;

namespace AppBackend.Data.Models
{
	public class Order_Shipment
	{
		public string OrderID { get; set; }
		public string ShipmentID { get; set; }
		[Column(TypeName = "decimal(18,3)")]
		public decimal Price { get; set; }
		[Column(TypeName = "decimal(18,3)")]
		public decimal Money { get; set; }
		public Order Order { get; set; }
		public Shipment Shipment { get; set; }
	}
}

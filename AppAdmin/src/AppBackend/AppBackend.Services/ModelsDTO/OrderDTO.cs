using AppBackend.Application.Common.Mappings;
using AppBackend.Data.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppBackend.Application.ModelsDTO
{
	public class OrderDTO : IMapFrom<Order>
	{
		public string Id { get; set; }
		public string EmployeeID { get; set; }
		public string Note { get; set; }
		public int Amount { get; set; }
		[Column(TypeName = "decimal(18,3)")]
		public decimal Price { get; set; }
		public string CategoryLongan { get; set; }
		public string Customer { get; set; }
		public ICollection<Order_Shipment> Order_Shipments { get; set; } = new List<Order_Shipment>();
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Order, OrderDTO>().ReverseMap();
		}
	}
}

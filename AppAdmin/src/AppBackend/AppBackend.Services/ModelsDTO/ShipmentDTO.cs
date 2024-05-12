using AppBackend.Application.Common.ConvertData;
using AppBackend.Application.Common.Mappings;
using AppBackend.Data.Models;
using AutoMapper;

namespace AppBackend.Application.ModelsDTO
{
	public class ShipmentDTO : IMapFrom<Shipment>
	{
		public string Id { get; set; }
		public string? EmployeeCode { get; set; }
		public string? Categoryname { get; set; }
		public string ShipmentCode { get; set; }
		public DateTimeOffset? DateFrom { get; set; }
		public DateTimeOffset? DateTo { get; set; }
		public DateTimeOffset? DateUp { get; set; }
		public string? EmployeeID { get; set; }
		public int? Status { get; set; }
		public string? StatusString { get; set; }
		public string? CategoryID { get; set; }
		public double Amount { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Shipment, ShipmentDTO>()
			 .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
			// Ánh xạ các trường thật
			.ForMember(d => d.ShipmentCode, opt => opt.MapFrom(s => s.ShipmentCode))
			.ForMember(d => d.DateFrom, opt => opt.MapFrom(s => s.DateFrom))
			.ForMember(d => d.DateTo, opt => opt.MapFrom(s => s.DateTo))
			.ForMember(d => d.DateUp, opt => opt.MapFrom(s => s.DateUp))
			.ForMember(d => d.EmployeeID, opt => opt.MapFrom(s => s.EmployeeID))
			.ForMember(d => d.Status, opt => opt.MapFrom(s => (s.Status)))
			.ForMember(d => d.CategoryID, opt => opt.MapFrom(s => s.CategoryID))
			.ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount))
			// Ánh xạ các trường ảo
			.ForMember(d => d.EmployeeCode, opt => opt.MapFrom(s => s.Employee.EmployeeCode))
			.ForMember(d => d.Categoryname, opt => opt.MapFrom(s => s.Category.Name))
			.ForMember(d => d.StatusString, opt => opt.MapFrom(s => ConvertData.ConvertStatusShipment(s.Status)));

			profile.CreateMap<ShipmentDTO, Shipment>();
			;

		}
	}
}

using AppBackend.Application.Common.Mappings;
using AppBackend.Data.Models;
using AutoMapper;

namespace AppBackend.Application.ModelsDTO
{
	public class PurchaseOrderDTO : IMapFrom<PurchaseOrder>
	{
		public string Id { get; set; }
		public string EmployeeID { get; set; }
		public string? EmployeeCode { get; set; }
		public DateTimeOffset OrderDate { get; set; }
		public int Amount { get; set; }
		public string? Note { get; set; }
		public double Price { get; set; }
		public string IngredientID { get; set; }
		public string? IngredientName { get; set; }
		/// <summary>
		/// public string? IsDeleted { get; set; }
		/// </summary>
		/// <param name="profile"></param>

		public void Mapping(Profile profile)
		{
			profile.CreateMap<PurchaseOrderDTO, PurchaseOrder>().ReverseMap()
				.ForMember(d => d.Price, otp => otp.MapFrom(s => (double)s.Price))
				.ForMember(d => d.EmployeeCode, otp => otp.MapFrom(s => s.Employee.EmployeeCode))
				.ForMember(d => d.IngredientName, otp => otp.MapFrom(s => s.Ingredient.Name))
				;

			//	profile.CreateMap<PurchaseOrder, PurchaseOrderDTO>().ForMember(d=>d.Price,otp=>otp.MapFrom(s=>s.Price));

			//profile.CreateMap<IngredientDTO, Ingredient>()
			//	.ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
			//	.ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
			//	.ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
			//	.ForMember(d => d.Measure, opt => opt.MapFrom(s => s.Measure));
		}
	}
}

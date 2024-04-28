using AppBackend.Application.Common.Mappings;
using AppBackend.Data.Models;
using AutoMapper;

namespace AppBackend.Application.ModelsDTO
{
	public class IngredientDTO : IMapFrom<Ingredient>
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string Measure { get; set; }


		public void Mapping(Profile profile)
		{
			profile.CreateMap<Ingredient, IngredientDTO>().ReverseMap();


			//profile.CreateMap<IngredientDTO, Ingredient>()
			//	.ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
			//	.ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
			//	.ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
			//	.ForMember(d => d.Measure, opt => opt.MapFrom(s => s.Measure));
		}
	}
}

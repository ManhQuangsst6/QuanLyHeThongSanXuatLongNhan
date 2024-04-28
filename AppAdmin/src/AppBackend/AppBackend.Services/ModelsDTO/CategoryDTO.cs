using AppBackend.Application.Common.Mappings;
using AppBackend.Data.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppBackend.Application.ModelsDTO
{
	public class CategoryDTO : IMapFrom<Category>
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; } = String.Empty;
		[Column(TypeName = "decimal(18,3)")]
		public decimal WholesalePrice { get; set; }
		[Column(TypeName = "decimal(18,3)")]
		public decimal RetailPrice { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Category, CategoryDTO>().ReverseMap();
		}
	}
}

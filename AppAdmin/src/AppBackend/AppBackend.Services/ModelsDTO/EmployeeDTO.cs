using AppBackend.Application.Common.Mappings;
using AppBackend.Data.Models;
using AutoMapper;

namespace AppBackend.Application.ModelsDTO
{
	public class EmployeeDTO : IMapFrom<Employee>
	{
		public string Id { get; set; }
		public string EmployeeCode { get; set; }
		public string? FullName { get; set; }
		public string? ImageLink { get; set; }
		public int Status { get; set; }
		public int? IsDeleted { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Employee, EmployeeDTO>().ReverseMap();
		}
	}
}

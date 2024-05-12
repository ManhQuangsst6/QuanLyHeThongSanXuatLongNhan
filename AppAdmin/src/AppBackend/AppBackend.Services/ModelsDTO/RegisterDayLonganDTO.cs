using AppBackend.Application.Common.Mappings;
using AppBackend.Data.Models;
using AutoMapper;

namespace AppBackend.Application.ModelsDTO
{
	public class RegisterDayLonganDTO : IMapFrom<RegisterDayLongan>
	{
		public string Id { get; set; }
		public string EmployeeID { get; set; }
		public string? EmployeeCode { get; set; }
		public string? EmployeeName { get; set; }
		public int Amount { get; set; }
		public string? Status { get; set; }
		public int? Ischeck { get; set; } = 0;
		public DateTimeOffset? Created { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<RegisterDayLonganDTO, RegisterDayLongan>()
				;
			profile.CreateMap<RegisterDayLongan, RegisterDayLonganDTO>();
		}
	}
}

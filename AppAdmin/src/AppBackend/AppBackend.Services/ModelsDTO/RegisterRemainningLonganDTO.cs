using AppBackend.Application.Common.Mappings;
using AppBackend.Data.Models;
using AutoMapper;

namespace AppBackend.Application.ModelsDTO
{
	public class RegisterRemainningLonganDTO : IMapFrom<RegisterRemainningLongan>
	{
		public string Id { get; set; }
		public string EmployeeID { get; set; }
		public string? EmployeeCode { get; set; }
		public string? EmployeeName { get; set; }
		public int Amount { get; set; }
		public int Ischeck { get; set; } = 0;
		public DateTimeOffset? Created { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<RegisterRemainningLongan, RegisterRemainningLonganDTO>()
				.ForMember(d => d.EmployeeCode, otp => otp.MapFrom(s => s.Employee.EmployeeCode))
				.ForMember(d => d.EmployeeName, otp => otp.MapFrom(s => s.Employee.FullName));
			profile.CreateMap<RegisterRemainningLonganDTO, RegisterRemainningLongan>();
		}
	}
}

using AppBackend.Application.Common.Mappings;
using AppBackend.Data.Models;
using AutoMapper;

namespace AppBackend.Application.ModelsDTO
{
	public class WorkAttendanceDTO : IMapFrom<WorkAttendance>
	{
		public string Id { get; set; }
		public string EmployeeID { get; set; }
		public string? EmployeeCode { get; set; }
		public string? EmployeeName { get; set; }
		public string? ListAmount { get; set; }
		public string? Note { get; set; }
		public int? SumAmount { get; set; }
		public int? IsSalary { get; set; } = 0;
		public int? ComfirmAmount { get; set; }
		public DateTimeOffset? Created { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<WorkAttendanceDTO, WorkAttendance>();

			profile.CreateMap<WorkAttendance, WorkAttendanceDTO>()
				.ForMember(d => d.EmployeeCode, opt => opt.MapFrom(s => s.Employee.EmployeeCode))
				.ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FullName));
		}
	}
}

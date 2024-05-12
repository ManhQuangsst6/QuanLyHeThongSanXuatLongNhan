using AppBackend.Application.Common.Mappings;
using AppBackend.Data.Models;
using AutoMapper;

namespace AppBackend.Application.ModelsDTO
{
	public class EventDTO : IMapFrom<Event>
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; }
		public DateTimeOffset DateStart { get; set; }
		public decimal Expense { get; set; }
		public string EmployeeID { get; set; }
		public string? EmployeeCode { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Event, EventDTO>()
				.ForMember(d => d.Expense, otp => otp.MapFrom(s => s.Expense))
				.ForMember(d => d.EmployeeCode, otp => otp.MapFrom(s => s.Employee.EmployeeCode));
			profile.CreateMap<EventDTO, Event>();
		}
	}
}

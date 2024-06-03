using AppBackend.Application.Common.Mappings;
using AppBackend.Data.Models;
using AutoMapper;

namespace AppBackend.Application.ModelsDTO
{
	public class NotificationDTO : IMapFrom<Notification>
	{
		public string ID { get; set; }
		public string? EmployeeReceive { get; set; }
		public string? EmployeeRole { get; set; }
		public string Content { get; set; }
		public string Link { get; set; }
		public string Image { get; set; }
		public int? IsRead { get; set; }
		public string? CreatedBy { get; set; }
		public DateTimeOffset? Created { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Notification, NotificationDTO>().ReverseMap();
		}
	}
}

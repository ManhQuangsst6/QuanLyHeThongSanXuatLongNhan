using AppBackend.Data.Common;

namespace AppBackend.Data.Models
{
	public class Position : BaseAuditableEntity
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
	}
}

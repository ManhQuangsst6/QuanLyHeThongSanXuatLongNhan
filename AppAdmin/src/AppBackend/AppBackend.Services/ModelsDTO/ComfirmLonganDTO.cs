using AppBackend.Application.Common.Mappings;
using AppBackend.Data.Models;

namespace AppBackend.Application.ModelsDTO
{
	public class ComfirmLonganDTO : IMapFrom<ComfirmLongan>
	{
		public string Id { get; set; }
		public string EmployeeID { get; set; }
		public double Amount { get; set; }
		public string? Note { get; set; }
		public int? IsComfirm { get; set; }
	}
}

using AppBackend.Application.Common.Mappings;
using AppBackend.Data.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppBackend.Application.ModelsDTO
{
	public class SalaryDTO : IMapFrom<Salary>
	{
		public string Id { get; set; }
		public string EmployeeID { get; set; }
		public string? EmployeeCode { get; set; }
		public string? EmployeeName { get; set; }
		public string? Note { get; set; }
		public int? QuarterYear { get; set; }
		public int? Year { get; set; }
		[Column(TypeName = "decimal(18,3)")]
		public decimal SalaryMoney { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Salary, SalaryDTO>();
			profile.CreateMap<SalaryDTO, Salary>();
		}
	}
}

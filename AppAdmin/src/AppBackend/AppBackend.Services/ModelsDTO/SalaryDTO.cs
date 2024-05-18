using AppBackend.Application.Common.ConvertData;
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
		public int SumAmount { get; set; }
		public int? Status { get; set; }
		public string? StatusString { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Salary, SalaryDTO>()
			.ForMember(d => d.EmployeeCode, otp => otp.MapFrom(s => s.Employee.EmployeeCode))
			.ForMember(d => d.EmployeeName, otp => otp.MapFrom(s => s.Employee.FullName))
			.ForMember(d => d.StatusString, otp => otp.MapFrom(s => ConvertData.ConvertStatusSalary(s.Status)));
			profile.CreateMap<SalaryDTO, Salary>();
		}
	}
}

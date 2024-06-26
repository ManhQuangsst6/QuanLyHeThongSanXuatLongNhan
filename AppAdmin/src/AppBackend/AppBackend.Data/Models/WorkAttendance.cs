﻿using AppBackend.Data.Common;

namespace AppBackend.Data.Models
{
	public class WorkAttendance : BaseAuditableEntity
	{
		public string Id { get; set; }
		public string EmployeeID { get; set; }
		public string? ListAmount { get; set; }
		public string? Note { get; set; }
		public int? SumAmount { get; set; }
		public int? ComfirmAmount { get; set; }
		public int? IsSalary { get; set; } = 0;
		public Employee Employee { get; set; }
	}
}

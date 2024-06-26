﻿namespace AppBackend.Application.Common.ConvertData
{
	public class ConvertData
	{
		public static string ConvertStatusShipment(int? stutus)
		{
			if (stutus == 0) return "Đóng hàng";
			if (stutus == 1) return "Đang bán";
			if (stutus == 2) return "Đã bán";
			return "Ngoại lệ";
		}
		public static string ConvertStatusRegisterLongan(int? stutus)
		{
			if (stutus == 2) return "Đã nhận";
			if (stutus == 1) return "Đang giao";
			if (stutus == 0) return "Đã lập";
			if (stutus == 3) return "Từ chối";
			if (stutus == 4) return "Hủy bỏ";
			return "Ngoại lệ";
		}
		public static string ConvertStatusRegisterRemainLongan(int? stutus)
		{
			if (stutus == 1) return "Đã trả";
			if (stutus == 0) return "Đã lập";
			if (stutus == 2) return "Hủy bỏ";
			return "Ngoại lệ";
		}
		public static string ConvertStatusSalary(int? stutus)
		{
			if (stutus == 0) return "Thanh toán";
			if (stutus == 1) return "Đã thanh toán";
			return "Ngoại lệ";
		}
	}
}

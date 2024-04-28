namespace AppBackend.Application.Common.ConvertData
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
	}
}

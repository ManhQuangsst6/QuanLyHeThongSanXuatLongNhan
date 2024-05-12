namespace AppBackend.Data.Enums
{
	public class EnumData
	{
		static string Done = "Đã nhận";
		static string Doing = "Đang giang giao";
		static string ToDo = "Đợi kiểm tra";

		public enum StatusEmployee
		{
			Active,
			Inactive,
			Leave

		}
		public enum StatusIsDelete
		{
			Doing,
			Done
		}
		public enum ComfirmWorkAmount
		{
			Success = 1,
			Fail = 2
		}


	}
}

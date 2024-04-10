namespace AppBackend.Application.Common.Response
{
	public class Response<T>
	{
		public int Status { get; set; }
		public string? Message { get; set; }
		public bool IsSuccess { get; set; }
		public T? Value { get; set; }
	}
}

namespace AppBackend.Application.ModelsDTO.ViewModel
{
	public class OrderViewModel
	{
		public List<string> Shipments { set; get; } = [];
		OrderDTO OrderDTO { set; get; }
	}
}

using Newtonsoft.Json;

namespace MlNetService.App.Dtos
{
	public class NominatimResponse
	{
		public string DisplayName { get; set; }
		public NominatimAddress Address { get; set; }
	}
}
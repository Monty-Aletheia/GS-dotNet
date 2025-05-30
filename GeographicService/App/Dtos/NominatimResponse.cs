using Newtonsoft.Json;

namespace GeographicService.App.Dtos
{
	public class NominatimResponse
	{
		public string DisplayName { get; set; }
		public NominatimAddress Address { get; set; }
	}
}

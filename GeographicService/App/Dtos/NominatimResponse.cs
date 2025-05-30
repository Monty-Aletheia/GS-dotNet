using Newtonsoft.Json;

namespace GeographicService.App.Dtos
{
	public class NominatimResponse
	{
		[JsonProperty("lat")]
		public string Lat { get; set; }

		[JsonProperty("lon")]
		public string Lon { get; set; }
	}
}

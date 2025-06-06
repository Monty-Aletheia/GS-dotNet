using Newtonsoft.Json;

namespace MlNetService.App.Dtos
{
	public class NominatimAddress
	{
		[JsonProperty("suburb")]
		public string Suburb { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("municipality")]
		public string Municipality { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("country_code")]
		public string CountryCode { get; set; }
	}



}
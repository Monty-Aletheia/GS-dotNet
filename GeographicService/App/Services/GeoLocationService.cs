using GeographicService.App.Dtos;
using Newtonsoft.Json;
using System.Net.Http;

namespace GeographicService.App.Services
{
	public class GeoLocationService
	{
		private readonly HttpClient _httpClient;

		public GeoLocationService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<(double Latitude, double Longitude)> GetCoordinatesAsync(string cityName, string state)
		{
			var url = $"https://nominatim.openstreetmap.org/search?city={Uri.EscapeDataString(cityName)}&state={Uri.EscapeDataString(state)}&country=Brazil&format=json&limit=1";

			_httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("GeographicService/1.0");

			var response = await _httpClient.GetAsync(url);
			response.EnsureSuccessStatusCode();

			var json = await response.Content.ReadAsStringAsync();

			var results = JsonConvert.DeserializeObject<List<NominatimResponse>>(json);

			if (results != null && results.Count > 0)
			{
				var result = results.First();
				return (double.Parse(result.Lat), double.Parse(result.Lon));
			}

			return (0, 0); // ou lançar exceção
		}

	}
}

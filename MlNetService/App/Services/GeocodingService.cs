using MlNetService.App.Dtos;
using MlNetService.Domain.Interfaces;
using System.Globalization;
using System.Text.Json;

namespace MlNetService.App.Services
{
	public class GeocodingService : IGeocodingService
	{
		private readonly HttpClient _httpClient;

		public GeocodingService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("https://nominatim.openstreetmap.org");
		}

		public async Task<NominatimAddress> ReverseGeocodeAsync(double latitude, double longitude)
		{

			var url = $"reverse?lat={latitude.ToString(CultureInfo.InvariantCulture)}&lon={longitude.ToString(CultureInfo.InvariantCulture)}&format=json&addressdetails=1";

			var request = new HttpRequestMessage(HttpMethod.Get, url);
			request.Headers.Add("User-Agent", "WatchTower/1.0 (https://github.com/queijoqualho contact: pedromra.04@gmail.com)");

			var response = await _httpClient.SendAsync(request);
			var responseContent = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"Erro {response.StatusCode}: {responseContent}");
			}

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			var nominatimResponse = JsonSerializer.Deserialize<NominatimResponse>(responseContent, options);
			return nominatimResponse.Address;
		}

		public async Task<(double Latitude, double Longitude)> GeocodeAsync(string municipio)
		{
			var url = $"search?q={Uri.EscapeDataString(municipio)}&format=json&limit=1";

			var request = new HttpRequestMessage(HttpMethod.Get, url);
			request.Headers.Add("User-Agent", "WatchTower/1.0 (https://github.com/queijoqualho contact: pedromra.04@gmail.com)");

			var response = await _httpClient.SendAsync(request);
			var responseContent = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"Erro {response.StatusCode}: {responseContent}");
			}

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			var geocodeResults = JsonSerializer.Deserialize<List<NominatimSearchResult>>(responseContent, options);

			if (geocodeResults == null || geocodeResults.Count == 0)
			{
				throw new Exception($"Município '{municipio}' não encontrado.");
			}

			var firstResult = geocodeResults.First();

			double lat = double.Parse(firstResult.Lat, CultureInfo.InvariantCulture);
			double lon = double.Parse(firstResult.Lon, CultureInfo.InvariantCulture);

			return (lat, lon);
		}

	}
}
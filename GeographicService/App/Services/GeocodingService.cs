using GeographicService.App.Dtos;
using GeographicService.Domain.Interfaces;
using System.Globalization;
using System.Text.Json;
using System.Globalization;

namespace GeographicService.App.Services
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

	}
}

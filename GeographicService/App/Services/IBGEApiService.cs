using GeographicService.App.Dtos;
using GeographicService.Domain.Models;
using Newtonsoft.Json;

namespace GeographicService.App.Services
{
	public class IBGEApiService
	{
		private readonly HttpClient _httpClient;

		public IBGEApiService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<City>> GetAllCitiesOfBrazilAsync()
		{
			var allCities = new List<City>();

			var ufs = new List<string>
			{
				"AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA",
				"MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN",
				"RS", "RO", "RR", "SC", "SP", "SE", "TO"
			};

			foreach (var uf in ufs)
			{
				var cities = await GetCityByIBGEApiAsync(uf);
				allCities.AddRange(cities);
			}

			return allCities;
		}

		public async Task<List<City>> GetCityByIBGEApiAsync(string uf)
		{
			var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{uf}/municipios";

			var response = await _httpClient.GetAsync(url);
			response.EnsureSuccessStatusCode();

			var json = await response.Content.ReadAsStringAsync();

			var cities = JsonConvert.DeserializeObject<List<CityIBGEResponse>>(json);

			var resultado = new List<City>();

			foreach (var m in cities)
			{
				resultado.Add(new City
				{
					Name = m.Nome,
					IBGECode = m.Id.ToString()
				});
			}

			return resultado;
		}
	}


}

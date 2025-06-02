using GeographicService.App.Dtos;

namespace GeographicService.Domain.Interfaces
{
	public interface IGeocodingService
	{
		Task<NominatimAddress> ReverseGeocodeAsync(double latitude, double longitude);
	}
}
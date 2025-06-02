using MlNetService.App.Dtos;

namespace MlNetService.Domain.Interfaces
{
	public interface IGeocodingService
	{
		Task<NominatimAddress> ReverseGeocodeAsync(double latitude, double longitude);
	}
}
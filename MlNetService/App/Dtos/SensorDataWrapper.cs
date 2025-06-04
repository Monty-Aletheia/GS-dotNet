using MlNetService.Domain.Models;

namespace MlNetService.App.Dtos
{
	public class SensorDataWrapper
	{
		public SensorData Sensor { get; set; }
		public LatLog Localizacao { get; set; }
	}

}

namespace MlNetService.App.Dtos.WebSocket
{
	public class LatLog
	{
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public LatLog(double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		public LatLog()
		{
		}

		public override string ToString()
		{
			return $"Lat: {Latitude}, Log: {Longitude}";
		}
	}
}
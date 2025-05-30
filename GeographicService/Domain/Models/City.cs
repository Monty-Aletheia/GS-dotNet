namespace GeographicService.Domain.Models
{
	public class City
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string State { get; set; }
		public string IBGECode { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}

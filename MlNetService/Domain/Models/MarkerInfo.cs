using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MlNetService.Domain.Models
{
	public class MarkerInfo
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; } = string.Empty;

		[BsonElement("latitude")]
		public double Latitude { get; set; }

		[BsonElement("longitude")]
		public double Longitude { get; set; }

		[BsonElement("desasterType")]
		public string DesasterType { get; set; }

		[BsonElement("markerType")]
		public string MarkerType { get; set; } = string.Empty;

		[BsonElement("markerName")]
		public string MarkerName { get; set; } = string.Empty;

		[BsonElement("markerImage")]
		public string MarkerImage { get; set; } = string.Empty;
	}
}

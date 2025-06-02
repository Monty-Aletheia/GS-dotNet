using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MlNetService.Domain.Models
{
	public class MakerInfo
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

		[BsonElement("makerType")]
		public string MakerType { get; set; } = string.Empty;

		[BsonElement("makerName")]
		public string MakerName { get; set; } = string.Empty;

		[BsonElement("makerImage")]
		public string MakerImage { get; set; } = string.Empty;
	}
}

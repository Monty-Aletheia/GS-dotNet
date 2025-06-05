using Microsoft.Extensions.Options;
using MlNetService.Domain.Models;
using MlNetService.Infra.Config;
using MlNetService.Infra.Messaging.Producer;
using MongoDB.Driver;

namespace MlNetService.App.Services
{
	public class MarkerInfoService
	{
		private readonly IMongoCollection<MarkerInfo> _collection;
		private readonly MarkerInfoProducer _makerInfoProducer;

		public MarkerInfoService(IOptions<MongoDBSettings> settings, IMongoClient mongoClient, MarkerInfoProducer _makerInfoProducer)
		{
			var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
			this._makerInfoProducer = _makerInfoProducer;
			_collection = database.GetCollection<MarkerInfo>("MakerInfos");
		}


		public List<MarkerInfo> Get() => _collection.Find(m => true).ToList();

		public MarkerInfo GetById(string id) => _collection.Find(m => m.Id == id).FirstOrDefault();

		public MarkerInfo Create(MarkerInfo makerInfo)
		{
			_collection.InsertOne(makerInfo);
			return makerInfo;
		}

		public void Update(string id, MarkerInfo updated) => _collection.ReplaceOne(m => m.Id == id, updated);

		public void Delete(string id) => _collection.DeleteOne(m => m.Id == id);

		async public void  PrepareMakerInfoAsync(string prediction, double lat, double log , SensorData? sensorData)
		{
			var marker = new MarkerInfo
			{
				Latitude = lat,
				Longitude = log,
				DesasterType = prediction,
				MarkerType = "Disaster",
				MarkerName = "Disaster Marker",
				MarkerImage = "https://example.com/disaster-marker.png",
				SensorData = sensorData
			};

			await _makerInfoProducer.SendMarkerInfoAsync(marker);
		}

	}
}

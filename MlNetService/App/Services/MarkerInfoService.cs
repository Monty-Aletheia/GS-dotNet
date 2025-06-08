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

		// WebSocket
		public async Task UpsertMarkerInfoAsync(string prediction, double lat, double lon, SensorData? sensorData)
		{
			var existingMarker = FindMarkerByLocation(lat, lon);

			if (!IsNormalPrediction(prediction))
			{
				if (existingMarker != null)
				{
					await UpdateMarkerAsync(existingMarker, prediction, sensorData);
				}
				else
				{
					var newMarker = CreateNewMarker(prediction, lat, lon, sensorData);
					await InsertMarkerAsync(newMarker);
				}
			}
			else if (existingMarker != null)
			{
				await _collection.DeleteOneAsync(m => m.Id == existingMarker.Id);
			}
		}

		// Mobile Ward
		public async Task UpsertMarkerInfoAsync(MarkerInfo markerInfo)
		{
			var existingMarker = FindMarkerByLocation(markerInfo.Latitude, markerInfo.Longitude);

			if (!IsNormalPrediction(markerInfo.DesasterType))
			{
				if (existingMarker != null)
				{
					existingMarker.Timestamp = GetCurrentTimestamp();
					markerInfo.Id = existingMarker.Id;
					await _collection.ReplaceOneAsync(m => m.Id == markerInfo.Id, markerInfo);
				}
				else
				{
					await _collection.InsertOneAsync(markerInfo);
				}
			}
			else if (existingMarker != null)
			{
				await _collection.DeleteOneAsync(m => m.Id == existingMarker.Id);
			}

		}

		// Helpers

		private MarkerInfo? FindMarkerByLocation(double lat, double lon)
		{
			return _collection.Find(m => m.Latitude == lat && m.Longitude == lon).FirstOrDefault();
		}

		private bool IsNormalPrediction(string prediction)
		{
			return prediction.Equals("normal", StringComparison.OrdinalIgnoreCase);
		}

		private async Task UpdateMarkerAsync(MarkerInfo marker, string prediction, SensorData? sensorData)
		{
			marker.DesasterType = prediction;
			marker.SensorData = sensorData;
			marker.Timestamp = GetCurrentTimestamp();

			await ReplaceMarkerAsync(marker);
		}

		private async Task ReplaceMarkerAsync(MarkerInfo marker)
		{
			await _collection.ReplaceOneAsync(m => m.Id == marker.Id, marker);
			await _makerInfoProducer.SendMarkerInfoAsync(marker);
		}

		private async Task InsertMarkerAsync(MarkerInfo marker)
		{
			marker.Timestamp ??= GetCurrentTimestamp();
			await _collection.InsertOneAsync(marker);
			await _makerInfoProducer.SendMarkerInfoAsync(marker);
		}

		private MarkerInfo CreateNewMarker(string prediction, double lat, double lon, SensorData? sensorData)
		{
			return new MarkerInfo
			{
				Latitude = lat,
				Longitude = lon,
				DesasterType = prediction,
				MarkerType = "Disaster",
				SensorData = sensorData,
				Timestamp = GetCurrentTimestamp()
			};
		}

		private string GetCurrentTimestamp()
		{
			return DateTime.UtcNow.ToString("o");
		}


		public async Task SendAllMarkersInfosAsync()
		{
			var markers = Get();
			await _makerInfoProducer.SendAllMarkersInfosAsync(markers);
		}

	}
}
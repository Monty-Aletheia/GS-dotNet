using Microsoft.Extensions.Options;
using MlNetService.Domain.Models;
using MlNetService.Infra.Config;
using MongoDB.Driver;

namespace MlNetService.App.Services
{
	public class MakerInfoService
	{
		private readonly IMongoCollection<MarkerInfo> _collection;

		public MakerInfoService(IOptions<MongoDBSettings> settings, IMongoClient mongoClient)
		{
			var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
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

	}
}

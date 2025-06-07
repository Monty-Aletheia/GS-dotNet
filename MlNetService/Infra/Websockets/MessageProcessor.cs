using MlNetService.App.Dtos.WebSocket;
using MlNetService.App.Services;
using MlNetService.Domain.Interfaces;
using MlNetService.Domain.Models;
using MlNetService.Infra.Interfaces.WebSockets;
using System.Text.Json;

namespace MlNetService.Infra.Websockets
{
	public class MessageProcessor : IMessageProcessor
	{
		private readonly ILogger<MessageProcessor> _logger;
		private readonly MlNetAppService _mlNetAppService;
		private readonly IGeocodingService _geocodingService;
		private readonly MarkerInfoService _markerInfoService;

		public MessageProcessor(
			ILogger<MessageProcessor> logger,
			MlNetAppService mlNetAppService,
			IGeocodingService geocodingService,
			MarkerInfoService markerInfoService)
		{
			_logger = logger;
			_mlNetAppService = mlNetAppService;
			_geocodingService = geocodingService;
			_markerInfoService = markerInfoService;
		}

		public async Task<string> ProcessAsync(string message)
		{
			try
			{
				var (sensorData, latLog) = DeserializeAndNormalizeSensorData(message);
				var prediction = _mlNetAppService.Predict(sensorData);

				var address = await _geocodingService.ReverseGeocodeAsync(latitude: latLog.Latitude, longitude: latLog.Longitude);
				var (lat, log) = await _geocodingService.GeocodeAsync(address.City);

				await _markerInfoService.UpsertMarkerInfoAsync(prediction, lat, log, sensorData);

				return JsonSerializer.Serialize(new { prediction, lat, log });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao processar mensagem");
				return JsonSerializer.Serialize(new { error = "Erro ao processar mensagem" });
			}
		}

		private (SensorData, LatLog) DeserializeAndNormalizeSensorData(string json)
		{
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			var wrapper = JsonSerializer.Deserialize<SensorDataWrapper>(json, options) ?? new SensorDataWrapper();

			var data = wrapper.Sensor ?? new SensorData();
			data.Evento = data.Evento ?? "";

			var latLog = wrapper.Localizacao ?? new LatLog();

			return (data, latLog);
		}
	}
}
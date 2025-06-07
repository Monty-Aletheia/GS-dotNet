using Microsoft.ML.Data;

namespace MlNetService.Domain.Models
{
	public class SensorData
	{
		[LoadColumn(0)] public float Temperatura { get; set; }
		[LoadColumn(1)] public float Umidade { get; set; }
		[LoadColumn(2)] public float Pressao { get; set; }
		[LoadColumn(3)] public float Vento { get; set; }
		[LoadColumn(4)] public float Chuva { get; set; }
		[LoadColumn(5)] public float NivelAgua { get; set; }
		[LoadColumn(6)] public float Gases { get; set; }
		[LoadColumn(7)] public float Luminosidade { get; set; }
		[LoadColumn(8)] public string Evento { get; set; }
	}
}
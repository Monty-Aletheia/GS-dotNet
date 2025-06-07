using Microsoft.ML;
using MlNetService.Domain.Models;

public class MlNetAppService
{
	private readonly MLContext _mlContext;
	private ITransformer _model;
	private PredictionEngine<SensorData, EventPrediction> _predictionEngine;

	public MlNetAppService()
	{
		_mlContext = new MLContext();
		LoadModel();
	}

	private void LoadModel()
	{
		string modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Infra", "IA", "modeloDesastres.zip");
		if (!File.Exists(modelPath))
		{
			throw new FileNotFoundException($"Modelo não encontrado em: {modelPath}");
		}
		_model = _mlContext.Model.Load(modelPath, out _);
		_predictionEngine = _mlContext.Model.CreatePredictionEngine<SensorData, EventPrediction>(_model);
	}

	public void TrainModel()
	{
		string datasetUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Infra/Data/dataset_desastres_faker.csv");

		IDataView dataView = _mlContext.Data.LoadFromTextFile<SensorData>(
			datasetUrl, hasHeader: true, separatorChar: ',');

		var partitions = _mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);

		var pipeline = _mlContext.Transforms.Concatenate("Features",
			nameof(SensorData.Temperatura),
			nameof(SensorData.Umidade),
			nameof(SensorData.Pressao),
			nameof(SensorData.Vento),
			nameof(SensorData.Chuva),
			nameof(SensorData.NivelAgua),
			nameof(SensorData.Gases),
			nameof(SensorData.Luminosidade))
		.Append(_mlContext.Transforms.NormalizeMinMax("Features"))
		.Append(_mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(SensorData.Evento)))
		.Append(_mlContext.MulticlassClassification.Trainers.LightGbm("Label", "Features"))
		.Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));


		_model = pipeline.Fit(partitions.TrainSet);
		_predictionEngine = _mlContext.Model.CreatePredictionEngine<SensorData, EventPrediction>(_model);

		var predictions = _model.Transform(partitions.TestSet);
		var metrics = _mlContext.MulticlassClassification.Evaluate(predictions);

		Console.WriteLine($"MicroAccuracy: {metrics.MicroAccuracy:F2}");
		Console.WriteLine($"MacroAccuracy: {metrics.MacroAccuracy:F2}");

		string iaDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Infra", "IA");
		Directory.CreateDirectory(iaDir);

		string modelPath = Path.Combine(iaDir, "modeloDesastres.zip");

		_mlContext.Model.Save(_model, dataView.Schema, modelPath);

		Console.WriteLine("Modelo treinado e salvo com sucesso!");
		Console.WriteLine($"Modelo salvo em: {modelPath}");
	}

	public string Predict(SensorData input)
	{
		if (_predictionEngine == null)
		{
			throw new InvalidOperationException("Modelo não carregado.");
		}

		var prediction = _predictionEngine.Predict(input);
		return prediction.PredictedEvent;
	}
}
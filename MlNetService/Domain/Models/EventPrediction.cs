using Microsoft.ML.Data;

namespace MlNetService.Domain.Models
{
	public class EventPrediction
	{
		[ColumnName("PredictedLabel")]
		public string PredictedEvent;
	}
}
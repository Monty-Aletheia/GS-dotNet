using MassTransit;
using Newtonsoft.Json.Linq;


namespace MlNetService.Infra.Messaging.Consumer
{
	public class IoTMessageConsumer : IConsumer<JObject>
	{
		public async Task Consume(ConsumeContext<JObject> context)
		{
			var json = context.Message;
			Console.WriteLine($"Recebido: {json}");

			await Task.CompletedTask;
		}
	}

}

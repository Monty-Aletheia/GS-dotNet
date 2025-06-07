namespace MlNetService.Infra.Interfaces.WebSockets
{
	public interface IMessageProcessor
	{
		Task<string> ProcessAsync(string message);
	}

}
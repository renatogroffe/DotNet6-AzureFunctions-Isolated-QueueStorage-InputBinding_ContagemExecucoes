using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FunctionAppProcessarContagem.Contagem;

namespace FunctionAppProcessarContagem;

public class ContagemConsumer
{
    private const string QUEUE_NAME = "queue-contagem";
    private readonly ILogger _logger;

    public ContagemConsumer(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ContagemConsumer>();
    }

    [Function(nameof(ContagemConsumer))]
    public void Run([QueueTrigger(QUEUE_NAME, Connection = "AzureWebJobsStorage")] ResultadoContador item)
    {
        _logger.LogInformation($"Valor recebido do contador: {item.ValorAtual}");
        _logger.LogInformation($"Fila: {QUEUE_NAME} | Mensagem recebida: {item.Mensagem}");
        _logger.LogInformation($"Dados: {JsonSerializer.Serialize(item)}");
    }
}

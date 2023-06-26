//***CODE BEHIND - BY RODOLFO.FONSECA***//
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CodeBehind.PadraoProjeto.Maquina
{
    /// <summary>
    /// Consumidor da mensagem enviada no bus
    /// </summary>
    public class MessageConsumer : IConsumer<Mensagem>
    {
        readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(ILogger<MessageConsumer> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// quem irá consumir a mensagem e processa-la
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Consume(ConsumeContext<Mensagem> context)
        {
            _logger.LogInformation($"Conteudo recebido => {context.Message.Conteudo}");

            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// DTO tipo de referência - ainda que a semântica padrão seja por valor, e ele é armazenado sempre no heap
    /// </summary>
    public record Mensagem
    {
        public string? Conteudo { get; set; }
    }
}

using MassTransit;
using Microsoft.Extensions.Logging;

namespace CodeBehind.PadraoProjeto.Maquina
{
    public class Message
    {
        public string Text { get; set; }
    }

    public class MessageConsumer :  IConsumer<Message>
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
        public Task Consume(ConsumeContext<Message> context)
        {
            _logger.LogInformation("Conteudo recebido {Text}", context.Message.Text);

            return Task.CompletedTask;
        }
    }
}

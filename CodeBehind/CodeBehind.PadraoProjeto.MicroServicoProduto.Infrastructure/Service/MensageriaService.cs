//***CODE BEHIND - BY RODOLFO.FONSECA***//

using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace CodeBehind.PadraoProjeto.MicroServicoProduto.Infrastructure.Service
{
    public interface IMensageriaService
    {
        Task<bool> ProduceAsync(string topic, string message);
    }

    public class MensageriaService: IMensageriaService
    {
        private readonly IConfiguration _configuration;
        private readonly IProducer<Null, string> _producer;

        public MensageriaService(IConfiguration configuration)
        {
            _configuration = configuration;

            var producerconfig = new ProducerConfig
            {
                BootstrapServers = _configuration["Kafka:BootstrapServers"]
            };

            _producer = new ProducerBuilder<Null, string>(producerconfig).Build();
        }

        public async Task<bool> ProduceAsync(string topic, string message)
        {
            var kafkamessage = new Message<Null, string> { Value = message, };

            await _producer.ProduceAsync(topic, kafkamessage);

            return true;
        }
    }
}


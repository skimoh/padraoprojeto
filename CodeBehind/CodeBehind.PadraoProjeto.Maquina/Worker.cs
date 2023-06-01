using MassTransit;
using Microsoft.Extensions.Hosting;

namespace CodeBehind.PadraoProjeto.Maquina
{
    public class Worker : BackgroundService
    {
        readonly IBus _bus;

        public Worker(IBus bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// JOB QUE IRÁ PUBLICAR UM CONTEUDO AO BARRAMENTO A CADA 1 SEGUNDO
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new Message { Text = $"Tempo {DateTimeOffset.Now}" }, stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}

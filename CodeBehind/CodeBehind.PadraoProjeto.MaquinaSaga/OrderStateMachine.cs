using CodeBehind.PadraoProjeto.MaquinaSaga.Event;
using CodeBehind.PadraoProjeto.MaquinaSaga.Models;
using MassTransit;

namespace CodeBehind.PadraoProjeto.MaquinaSaga
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public readonly ILogger<OrderStateMachine> _logger;
        public OrderStateMachine(ILogger<OrderStateMachine> logger)
        {
            _logger = logger;

            InstanceState(x => x.CurrentState);

            Event(() => EventoEnviado, x => x.CorrelateById(x => x.Message.CurrelationId));
            Event(() => EventoProcessado, x => x.CorrelateById(x => x.Message.CurrelationId));
            Event(() => EventoCancelado, x => x.CorrelateById(x => x.Message.CurrelationId));
            Event(() => EventoCompletado, x => x.CorrelateById(x => x.Message.CurrelationId));

            Event(() => EventoEnviado, e =>
            {
                e.CorrelateById(x => x.Message.CurrelationId);

                e.InsertOnInitial = true;
                e.SetSagaFactory(c => new OrderState
                {
                    ResponseAddress = c.ResponseAddress,
                    SubmitDate = c.Message.Timestamp,
                    CorrelationId = c.Message.CurrelationId,
                    RequestId = c.Message.RequestId,
                    InfoComplementar = "Saga Rodolfo"
                });
            });

            Request(() => ProcessOrder, x => x.RequestId, config => { config.Timeout = TimeSpan.Zero; });

            Initially(
                When(EventoEnviado)
                  .Then(context =>
                  {
                      context.Saga.CorrelationId = context.Message.CurrelationId;
                      context.Saga.Updated = context.Message.Timestamp;
                  })
                  .Then(x => _logger.LogInformation("EventoEnviado"))
                  .ThenAsync(MetodoEnviar)
                  .TransitionTo(StatusProcessado)
              );

            During(StatusProcessado,
                When(EventoProcessado)
                  .Then(x => _logger.LogInformation("EventoProcessado"))
                  .ThenAsync(MetodoProcessar)
                  .TransitionTo(StatusCompletado),
                When(EventoCancelado)
                  .Then(x => _logger.LogInformation("StatusCancelado"))
                  .ThenAsync(MetodoCancelar)
                  .Finalize()
                );

            During(StatusCompletado,
                When(EventoCompletado)
                    .Then(x => _logger.LogInformation("StatusCompletado"))
                    .ThenAsync(MetodoCompletado)
                    .Finalize()
                );            
        }

        private async Task MetodoCancelar(BehaviorContext<OrderState, IPedidoCancelado> context)
        {
            _logger.LogInformation("Entrou no MetodoCancelar");
            
            var client = await context.GetSendEndpoint(context.Saga.ResponseAddress);

            var resp = new PedidoRetorno
            {
                Status = 0,
                Message = "Falha",
            };

            await client.Send(resp, r => r.RequestId = context.Saga.RequestId);
        }


        private async Task MetodoCompletado(BehaviorContext<OrderState, IPedidoCompletado> context)
        {
            _logger.LogInformation("Entrou no MetodoCompletado");

            var client = await context.GetSendEndpoint(context.Saga.ResponseAddress);

            var resp = new PedidoRetorno
            {
                Status = 1,
                Message = "Sucesso"
            };

            await client.Send(resp, r => r.RequestId = context.Saga.RequestId);
        }

        private Task MetodoProcessar(BehaviorContext<OrderState, IPedidoProcessado> context)
        {
            _logger.LogInformation("Entrou no MetodoProcessar");

            return context.Publish<IPedidoCompletado>(new
            {
                CurrelationId = context.Message.CurrelationId,
                Timestamp = DateTime.Now                
            });
        }

        private Task MetodoEnviar(BehaviorContext<OrderState, IPedidoEnviado> context)
        {
            _logger.LogInformation("Entrou no MetodoEnviar");

            return context.Publish<IPedidoProcessado>(new
            {
                CurrelationId = context.Message.CurrelationId,
                Timestamp = DateTime.Now
            });
        }

        public State StatusEnviado { get; private set; }
        public State StatusProcessado { get; private set; }
        public State StatusCancelado { get; private set; }
        public State StatusCompletado { get; private set; }

        public Event<IPedidoEnviado> EventoEnviado { get; private set; }
        public Event<IPedidoProcessado> EventoProcessado { get; private set; }
        public Event<IPedidoCancelado> EventoCancelado { get; private set; }
        public Event<IPedidoCompletado> EventoCompletado { get; private set; }

        public Request<OrderState, SagaRequest, PedidoRetorno> ProcessOrder { get; set; }
    }
}

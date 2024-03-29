﻿//***CODE BEHIND - BY RODOLFO.FONSECA***//

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
                    CorrelationId = c.Message.CurrelationId,
                    RequestId = c.Message.RequestId,
                    ResponseAddress = c.ResponseAddress,                    
                    SubmitDate = c.Message.DataOcorrencia,
                    
                    CodigoPedido = c.Message.CodigoPedido,
                    InfoComplementar = c.Message.Info
                });
            });

            Request(() => ProcessOrder, x => x.RequestId, config => { config.Timeout = TimeSpan.Zero; });

            Initially(
                When(EventoEnviado)
                  .Then(context =>
                  {
                      context.Saga.CorrelationId = context.Message.CurrelationId;
                      context.Saga.Updated = DateTime.Now;
                      context.Saga.RequestId = context.RequestId;
                      context.Saga.CodigoPedido = context.Message.CodigoPedido;
                  })
                  .Then(x => _logger.LogInformation("=>EventoEnviado"))
                  .ThenAsync(MetodoEnviar)
                  .TransitionTo(StatusProcessado)
              );

            During(StatusProcessado,
                When(EventoProcessado)
                  .Then(x => _logger.LogInformation("=>EventoProcessado"))
                  .ThenAsync(MetodoProcessar)
                  .TransitionTo(StatusCompletado),
                When(EventoCancelado)
                  .Then(x => _logger.LogInformation("=>StatusCancelado"))
                  .ThenAsync(MetodoCancelar)
                  .Finalize()
                );

            During(StatusCompletado,
                When(EventoCompletado)
                    .Then(x => _logger.LogInformation("=>StatusCompletado"))
                    .ThenAsync(MetodoCompletado)
                    .Finalize()
                );
        }

        #region METODOS
        
        private async Task MetodoCancelar(BehaviorContext<OrderState, IPedidoCancelado> context)
        {
            _logger.LogInformation("=>Entrou no MetodoCancelar");

            context.Saga.Updated = DateTime.Now;

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
            _logger.LogInformation($"=>Entrou no MetodoCompletado {context.Message.CodigoPedido}");

            context.Saga.Updated = DateTime.Now;

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
            _logger.LogInformation($"=>Entrou no MetodoProcessar {context.Message.CodigoPedido}");

            context.Saga.Updated = DateTime.Now;
            
            return context.Publish<IPedidoCompletado>(new
            {
                CurrelationId = context.Message.CurrelationId,
                RequestId = context.Saga.RequestId,
                
                DataOcorrencia = DateTime.Now,
                CodigoPedido = context.Message.CodigoPedido
            });
        }

        private Task MetodoEnviar(BehaviorContext<OrderState, IPedidoEnviado> context)
        {
            _logger.LogInformation($"=>Entrou no MetodoEnviar {context.Message.CodigoPedido}");

            context.Saga.Updated = DateTime.Now;

            return context.Publish<IPedidoProcessado>(new
            {
                CurrelationId = context.Message.CurrelationId,
                RequestId = context.Saga.RequestId,

                DataOcorrencia = DateTime.Now,
                CodigoPedido = context.Message.CodigoPedido
            });
        }

        #endregion

        #region STATUS
        
        public State StatusEnviado { get; private set; }
        public State StatusProcessado { get; private set; }
        public State StatusCancelado { get; private set; }
        public State StatusCompletado { get; private set; }

        #endregion

        public Event<IPedidoEnviado> EventoEnviado { get; private set; }
        public Event<IPedidoProcessado> EventoProcessado { get; private set; }
        public Event<IPedidoCancelado> EventoCancelado { get; private set; }
        public Event<IPedidoCompletado> EventoCompletado { get; private set; }

        public Request<OrderState, SagaRequest, PedidoRetorno> ProcessOrder { get; set; }
    }
}
